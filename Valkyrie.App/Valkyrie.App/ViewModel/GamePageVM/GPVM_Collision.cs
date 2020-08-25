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

            if(left)
            {
                // can we move left? 

                // if yes, then increase the acceleration rate by default

                actor.X_Acceleration_Rate -= actor.DefaultXAccelRate;

                // modify the acceleration by any buffs or debuffs here
            }

            //--------------------------------------------------------------------

            bool right = actor.ControlStatus.DirectionalStatus.R;

            if(right)
            {
                // can we move right? 

                // if yes, then increase the acceleration rate by default

                actor.X_Acceleration_Rate += actor.DefaultXAccelRate;

                // modify the acceleration by any buffs or debuffs here.
            }
        }
    }
}
