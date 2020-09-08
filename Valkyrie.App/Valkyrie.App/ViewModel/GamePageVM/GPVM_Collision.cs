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
                // is left being pressed? 

                bool left = actor.ControlStatus.DirectionalStatus.L;
                bool right = actor.ControlStatus.DirectionalStatus.R;

                if (left)
                {
                    // can we move left? 
                    // if yes, then increase the acceleration rate by default

                    actor.X_Acceleration_Rate -= actor.DefaultXAccelRate;

                    // modify the acceleration by any buffs or debuffs here

                    foreach (var obstacle in obstacles_)
                    {
                        if (actor.Intersects(obstacle))
                        {
                            // move to the right boundary and then stop moving

                            actor.StopXAxisMotion();
                            float newX = obstacle.Rectangle.Right;
                            GLPosition newPosition = new GLPosition(newX, actor.GLPosition.Y);
                            actor.MoveTo(newPosition);
                        }
                    }
                }

                else if (right)
                {
                    // can we move right? 
                    // if yes, then increase the acceleration rate by default

                    actor.X_Acceleration_Rate += actor.DefaultXAccelRate;

                    // modify the acceleration by any buffs or debuffs here.

                    // collision detection here
                }

                else
                {
                    // neither left or right is selected. 
                    // need to decelerate until speed = 0
                    // maybe I need a decelerate function? 


                    /*
                     */

                    var speed = Math.Abs(actor.x_speed);
                    var acceleration = Math.Abs(actor.X_Acceleration_Rate);

                    if (speed > 0 || acceleration > 0)
                    {
                        actor.Decelerate_X_Axis();
                    }
                }
            }
        }
    }
}
