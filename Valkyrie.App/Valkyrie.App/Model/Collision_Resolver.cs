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
using Valkryie.GL;
using Windows.Storage.Search;

namespace Valkyrie.App.Model
{
    public partial class Collision_Resolver
    {
        internal List<ICollidable> obstacles_ = new List<ICollidable>();
        public List<ICollidable> Obstacles
        {
            get => obstacles_;
            set => obstacles_ = value;
        }

        //==============================================================

        public void Add_Obtstacle(Obstacle obstacle)
        {
            obstacles_.Add(obstacle);
        }

        //=============================================================

        public int Count
        {
            get => obstacles_.Count;
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
            bool left_commanded = actor.ControlStatus.DirectionalStatus.L;
            bool right_commanded = actor.ControlStatus.DirectionalStatus.R;

            //--------------------------------------------

            if (left_commanded)
            {
                if (!actor.ObstructedLeft)
                {
                    EvaluateLeft(actor);
                }

                if(actor.ObstructedRight)
                {
                    actor.ObstructedRight = false;
                }
            }

            //--------------------------------------------

            else if (right_commanded)
            {
                if (!actor.ObstructedRight)
                {
                    EvaluateRight(actor);
                }

                if(actor.ObstructedLeft)
                {
                    actor.ObstructedLeft = false;
                }
            }

            //--------------------------------------------

            else
            {
                // neither left or right is selected. 
                // need to decelerate until speed = 0

                var speed = Math.Abs(actor.x_speed);
                var acceleration = Math.Abs(actor.X_Acceleration_Rate);

                if (speed > 0 || acceleration > 0)
                {
                    actor.Decelerate_X_Axis();
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

                var contextQuery = from obstacle in obstacles_
                                   where obstacle.Is_Left_Of(actor)
                                   orderby actor.Horizontal_Distance_Left(obstacle) ascending
                                   select obstacle;

                if(contextQuery.Any())
                {
                    var nearest = contextQuery.First();

                    if(actor.Intersects_Uncertainty_Region(nearest))
                    {
                        actor.ObstructedLeft = true;
                        actor.StopXAxisMotion();
                    }
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
                actor.X_Acceleration_Rate += actor.DefaultXAccelRate;

                var contextQuery = from obstacle in obstacles_
                                   where obstacle.Is_Right_Of(actor)
                                   orderby actor.Horizontal_Distance_Right(obstacle) ascending
                                   select obstacle;

                if(contextQuery.Any())
                {
                    var nearest = contextQuery.First() as Entity;

                    if (actor.Intersects_Uncertainty_Region(nearest))
                    {
                        actor.ObstructedRight = true;
                        actor.StopXAxisMotion();
                    }
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
            if (!actor.Standing)
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

                //if (actor.Is_About_To_Intersect_Y(nearest))                
                
                if (actor.Intersects_Uncertainty_Region(nearest))
                {
                    float newY = nearest.Rectangle.Bottom;
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
            actor.Y_Acceleration_Rate -= 1.0f;

            var contextQuery = from obstacle in obstacles_
                               where obstacle.Is_Below(actor)
                               orderby actor.Vertical_Distance_Below(obstacle) ascending
                               select obstacle;

            if (contextQuery.Any())
            {
                Obstacle nearest = (Obstacle)contextQuery.First();

                //---- condition #1: we are already intersecting

                
                //if (actor.Intersects_Uncertainty_Region(nearest))
                //if (actor.Is_About_To_Intersect_Y(nearest))
                
                if (actor.Intersects(nearest))
                {
                    //-- stop moving 

                    float newY = nearest.Rectangle.Top;
                    GLPosition newPosition = new GLPosition(actor.GLPosition.X, newY);
                    actor.MoveTo(newPosition);
                    actor.Land();
                }
            }
        }
    }
}
