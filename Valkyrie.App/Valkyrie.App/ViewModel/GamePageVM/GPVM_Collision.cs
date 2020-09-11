using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Valkryie.GL;
using Valkyrie.App.Model;
using System.Linq;
using System.Security.Cryptography;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        //===================================================================

        /*-------------------------------------
         * 
         * Evaluate Motion
         * 
         * -----------------------------------*/

        public void EvaluateMovement()
        {


            foreach(var actor in actors_)
            {
                EvaluateHorizontalMotion(actor);
                
                //EvaluateVerticalMotion(actor);
                
                actor.Accelerate();
            }
        }

        //===================================================================

        /*-------------------------------------
         * 
         * Evaluate Vertical Motion
         * 
         * 
         * 
         * -----------------------------------*/

        internal void EvaluateVerticalMotion(Actor actor)
        {

        }

        //===================================================================

        /*--------------------------------------------
         * 
         * Evaluate Horizontal Motion
         * 
         * -----------------------------------------*/

        internal void EvaluateHorizontalMotion(Actor actor)
        {
            if (!actor.Stationary)
            {
                bool left_commanded = actor.ControlStatus.DirectionalStatus.L;
                bool right_commanded = actor.ControlStatus.DirectionalStatus.R;

                //--------------------------------------------

                if (left_commanded)
                {
                    if(!actor.ObstructedLeft)
                    {
                        EvaluateLeft(actor);
                    }
                } 

                //--------------------------------------------

                else if (right_commanded)
                {
                    if(!actor.ObstructedRight)
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

        //====================================================================

        /*-------------------------------------
         * 
         * 
         * 
         * -----------------------------------*/

        internal void EvaluateLeft(Actor actor)
        {
            if(!actor.ObstructedLeft)
            {
                // can we move left? 
                // if yes, then increase the acceleration rate by default

                actor.X_Acceleration_Rate -= actor.DefaultXAccelRate;
                float newXSpeed = actor.Accelerate_X();

                //-- get a list of nearby obstacles in direction of travel

                var contextQuery = from obstacle in obstacles_
                                   where obstacle.Rectangle.Origin.X < actor.GLPosition.X
                                   orderby obstacle.GLPosition.Horizontal_Distance_To(actor.GLPosition) ascending
                                   select obstacle;

                Obstacle nearest = contextQuery.First();

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
                                   where obstacle.Rectangle.Origin.X > actor.Rectangle.Right
                                   orderby obstacle.GLPosition.Horizontal_Distance_To(actor.GLPosition) ascending
                                   select obstacle;

                Obstacle nearest = contextQuery.First();

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
    }
}
