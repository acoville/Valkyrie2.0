/*==========================================================
 * 
 * class goals
 * 
 *  1 - offload logic from the GamePageViewModel
 *  2 - provide benchmark logging for the time complexity
 *      of a given collision hash
 *  3 - provide a system to alter in-game gravity
 * 
 * =======================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valkryie.GL;

namespace Valkyrie.App.Model
{
    public partial class Collision_Resolver
    {
        internal List<ICollidable> obstacles_;
        public List<ICollidable> Obstacles
        {
            get => obstacles_;
            set => obstacles_ = value;
        }

        //==============================================================

        public Collision_Resolver()
        {
            obstacles_ = new List<ICollidable>();
        }

        //==============================================================

        public void Add_Obtstacle(Obstacle obstacle)
        {
            obstacles_.Add(obstacle);
        }

        //=============================================================

        public int Count
        {
            get
            {
                return obstacles_.Count;
            }
        }

        //============================================================

        /*--------------------------------
         * 
         * [ ] subscript operator is not
         * overloadable in C#, so this
         * is considered an "indexer" 
         * instead
         * 
         *-------------------------------*/

        public ICollidable this[int i]
        {
            get => obstacles_[i];
            set => obstacles_[i] = value;
        }

        //==============================================================

        public void EvaluateMotion(Actor actor)
        {
            EvaluateHorizontalMotion(actor);
            EvaluateVerticalMotion(actor);
        }

        //==============================================================

        internal void EvaluateHorizontalMotion(Actor actor)
        {
            if (!actor.Stationary)
            {
                bool left_commanded = actor.ControlStatus.DirectionalStatus.L;
                bool right_commanded = actor.ControlStatus.DirectionalStatus.R;

                //--------------------------------------------

                if (left_commanded)
                {
                    if (!actor.ObstructedLeft)
                    {
                        EvaluateLeft(actor);
                    }
                }

                //--------------------------------------------

                else if (right_commanded)
                {
                    if (!actor.ObstructedRight)
                    {
                        EvaluateRight(actor);
                    }
                }

                //--------------------------------------------

                else
                {
                    // neither left or right is selected. 
                    // need to decelerate until speed = 0
                    // maybe I need a decelerate function? 

                    /*
                     * THIS IS CAUSING A WEIRD GLITCH WHERE AN ACTOR
                     * UP AGAINST AN OBSTACLE BRIEFLY ACCELERATES INTO IT / 
                     * PAST IT WHEN THE DIRECTIONAL BUTTON IS RELEASED.
                     * 
                    var speed = Math.Abs(actor.x_speed);
                    var acceleration = Math.Abs(actor.X_Acceleration_Rate);

                    if (speed > 0 || acceleration > 0)
                    {
                        actor.Decelerate_X_Axis();
                    }
                     */

                    actor.StopXAxisMotion();
                }
            }
        }

        //===============================================================

        /*-------------------------------------
        * 
        * HorizontalMotion.EvaluateLeft
        * 
        * -----------------------------------*/

        internal void EvaluateLeft(Actor actor)
        {
            if (!actor.ObstructedLeft)
            {
                // can we move left? 
                // if yes, then increase the acceleration rate by default

                actor.X_Acceleration_Rate -= actor.DefaultXAccelRate;
                float newXSpeed = actor.Accelerate_X();

                //-- get a list of nearby obstacles in direction of travel

                var contextQuery = from obstacle in obstacles_
                                   where obstacle.Is_Left_Of(actor)
                                   orderby obstacle.Clearance_Left(actor) ascending
                                   select obstacle;

                Obstacle nearest = (Obstacle)contextQuery.First();

                // only need to evaluat the closest obstacle in direction of travel

                if (actor.Intersects(nearest))
                {
                    // move to the right boundary and then stop moving

                    float newX = nearest.Rectangle.Right;
                    GLPosition newPosition = new GLPosition(newX, actor.GLPosition.Y);
                    actor.MoveTo(newPosition);

                    actor.StopXAxisMotion();
                    actor.ObstructedLeft = true;
                }
            }
        }

        //=====================================================================

        /*--------------------------------
         * 
         * HorizontalMotion.EvaluateRight
         * 
         * ------------------------------*/

        internal void EvaluateRight(Actor actor)
        {
            // can we move right? 
            // if yes, then increase the acceleration rate by default

            if (!actor.ObstructedRight)
            {
                // can we move left? 
                // if yes, then increase the acceleration rate by default

                actor.X_Acceleration_Rate += actor.DefaultXAccelRate;
                float newXSpeed = actor.Accelerate_X();

                //-- get a list of nearby obstacles in direction of travel

                var contextQuery = from obstacle in obstacles_
                                   where obstacle.Is_Right_Of(actor)
                                   orderby obstacle.Clearance_Right(actor) ascending
                                   select obstacle;

                Obstacle nearest = (Obstacle)contextQuery.First();

                // only need to evaluat the closest obstacle in direction of travel

                if (actor.Intersects(nearest))
                {
                    // move to the left boundary and then stop moving

                    float newX = nearest.Rectangle.Left + actor.Rectangle.PixelWidth;

                    GLPosition newPosition = new GLPosition(newX, actor.GLPosition.Y);

                    actor.MoveTo(newPosition);

                    actor.StopXAxisMotion();
                    actor.ObstructedRight = true;
                }
            }
        }

        //===============================================================

        /*--------------------------------
         * 
         * main node of evaluate vertical
         * motion. Determines weather to 
         * initiate a jump or evaluate 
         * up / down 
         * 
         * -----------------------------*/

        internal void EvaluateVerticalMotion(Actor actor)
        {
            if (actor.Standing)
            {
                bool jumpCommanded = actor.ControlStatus.Jump;

                if (jumpCommanded)
                {
                    if (actor.AvailableJumps > 0)
                    {
                        actor.Jump();
                    }
                }
            }

            //------ otherwise, actor is not standing so 
            //         must be going up or down 

            else // actor is not standing
            {
                if (actor.Y_Speed > 0)
                {
                    EvaluateUp(actor);
                }
                else
                {
                    EvaluateDown(actor);
                }
            }
        }

        //================================================================

        /*--------------------------------
        * 
        * if Y_Speed > 0 then the actor 
        * is still moving up 
        * 
        * -----------------------------*/

        public void EvaluateUp(Actor actor)
        {
            actor.Y_Acceleration_Rate -= 1.0f;

            var contextQuery = from obstacle in obstacles_
                               where obstacle.Is_Above(actor)
                               orderby actor.Vertical_Distance_Above(obstacle) ascending
                               select obstacle;

            if (contextQuery.Any())
            {
                Obstacle nearest = (Obstacle)contextQuery.First();

                if (actor.Intersects(nearest))
                {
                    float newY = nearest.GLPosition.Y;

                    GLPosition newPosition = new GLPosition(actor.GLPosition.X, newY);
                    actor.MoveTo(newPosition);
                    actor.Stop_Y_Axis_Motion();
                }
            }
        }

        //=========================================================================

        /*--------------------------------
         * 
         * if Y_Speed is negative then 
         * the actor must be falling
         * 
         * -----------------------------*/

        public void EvaluateDown(Actor actor)
        {
            var contextQuery = from obstacle in obstacles_
                               where obstacle.Is_Below(actor)
                               orderby actor.Vertical_Distance_Below(obstacle) ascending
                               select obstacle;

            if (contextQuery.Any())
            {
                Obstacle nearest = (Obstacle)contextQuery.First();

                //---- condition #1: we are already intersecting

                if (actor.Intersects(nearest))
                {
                    //-- stop moving 

                    float newY = nearest.GLPosition.Y + nearest.Rectangle.PixelHeight;
                    GLPosition newPosition = new GLPosition(actor.GLPosition.X, newY);
                    actor.MoveTo(newPosition);
                    actor.Land();
                }

                //---- condition #2: we will be intersecting in the next frame

                else
                {
                    var vertical_clearance = actor.Vertical_Distance_Below(nearest);
                    var newYSpeed = actor.NextDeltaY();

                    var margin = vertical_clearance - newYSpeed;

                    if (margin <= 0)
                    {
                        //-- stop moving 

                        float newY = nearest.GLPosition.Y + nearest.Rectangle.PixelHeight;
                        GLPosition newPosition = new GLPosition(actor.GLPosition.X, newY);
                        actor.MoveTo(newPosition);
                        actor.Land();
                    }
                }
            }
        }
    }
}
