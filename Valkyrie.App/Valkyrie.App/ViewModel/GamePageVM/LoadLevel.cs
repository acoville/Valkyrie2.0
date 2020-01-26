/*================================================================
 * 
 * Valkyrie
 * Game Page View Model class
 * 
 * Helper Function to load all resources from a new level
 * 
 * =============================================================*/

using System.ComponentModel;
using Valkryie.GL;

namespace Valkyrie.App.ViewModel
{ 
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        internal void LoadLevel(Level map)
        {
            //---------------------------------------------------
            // set background image

            var ImagePath = "Valkyrie.App.Images.Backgrounds." + map.ImageSource;
            BackgroundImage = ImagePath;
        }
    }
}
 