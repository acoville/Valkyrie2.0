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

            Assembly assembly = GetType().GetTypeInfo().Assembly;
            var ImagePath = "Valkyrie.App.Images.Backgrounds." + map.ImageSource;

            using(Stream stream = assembly.GetManifestResourceStream(ImagePath))
            {
                //BackgroundImage = ImageSource.FromResource(ImagePath, assembly);
            }
        }
    }
}
 