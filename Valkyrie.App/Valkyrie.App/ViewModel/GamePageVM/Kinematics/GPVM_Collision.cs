using System.ComponentModel;
using Valkyrie.App.Model;

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
                EvaluateVerticalMotion(actor);              
                actor.Accelerate();
            }
        }
    }
}
