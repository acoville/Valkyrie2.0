using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Valkyrie.App.Model;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        //===================================================================

        /*-------------------------------------
         * 
         * Evaluate Motion and Collision
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
         * Evaluate Vertical Motion and Collision
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
         * Evaluate Horizontal Motion and Collision
         * 
         * 
         * 
         * -----------------------------------------*/

        internal void EvaluateHorizontalMotion(Actor actor)
        {
            // is left being pressed? 
            
            bool left = actor.ControlStatus.DirectionalStatus.L;
            bool right = actor.ControlStatus.DirectionalStatus.R;

            if(left)
            {
                // can we move left? 
                // if yes, then increase the acceleration rate by default

                actor.X_Acceleration_Rate -= actor.DefaultXAccelRate;

                // modify the acceleration by any buffs or debuffs here
            }

            else if(right)
            {
                // can we move right? 
                // if yes, then increase the acceleration rate by default

                actor.X_Acceleration_Rate += actor.DefaultXAccelRate;

                // modify the acceleration by any buffs or debuffs here.
            }

            else
            {
                // neither left or right is selected. 
                // need to decelerate until speed = 0
                // maybe I need a decelerate function? 

                if(Math.Abs(actor.X_Acceleration_Rate) > 0)
                {
                    actor.Decelerate();
                }
            }

        }
    }
}
