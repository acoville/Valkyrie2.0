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

        public async Task EvaluateMovement()
        {
            foreach(var actor in actors_)
            {

                await EvaluateVerticalMotion(actor);
                await EvaluateHorizontalMotion(actor);
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

        internal async Task EvaluateVerticalMotion(Actor actor)
        {

        }

        //===================================================================

        /*-------------------------------------
         * 
         * Evaluate Horizontal Motion and Collision
         * 
         * 
         * 
         * -----------------------------------*/

        internal async Task EvaluateHorizontalMotion(Actor actor)
        {

        }
    }
}
