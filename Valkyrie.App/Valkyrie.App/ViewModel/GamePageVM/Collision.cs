using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
                EvaluateVerticalMotion(actor);
                EvaluateHorizontalMotion(actor);
            }
        }

        //===================================================================

        /*-------------------------------------
         * 
         * Evaluate Vertical Motion and Collision
         * 
         * -----------------------------------*/

        internal void EvaluateVerticalMotion(Actor actor)
        {

        }

        //===================================================================

        /*-------------------------------------
         * 
         * Evaluate Horizontal Motion and Collision
         * 
         * -----------------------------------*/

        internal void EvaluateHorizontalMotion(Actor actor)
        {

        }
    }
}
