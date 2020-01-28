/*================================================================
 * 
 * Valkyrie
 * Game Page View Model class
 * 
 * Helper Function to load all resources from a new level
 * 
 * =============================================================*/

using SkiaSharp;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Valkryie.GL;

namespace Valkyrie.App.ViewModel
{ 
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        internal void LoadLevel(Level map)
        {
            //---------------------------------------------------
            // set background image

            //DeviceScreen.BackgroundSource = map.ImageSource;

            var path = "Valkyrie.App.Images.Backgrounds." + map.ImageSource;
            BackgroundImage = ImageSource.FromResource(path);
        }
    }
}
 