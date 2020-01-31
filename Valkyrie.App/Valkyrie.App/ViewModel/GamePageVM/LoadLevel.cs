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
using Valkyrie.Graphics;

namespace Valkyrie.App.ViewModel
{ 
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        internal void LoadLevel(Level map)
        {
            //-----------------------------------------------
            // set background image

            var path = "Valkyrie.App.Images.Backgrounds." + map.BackgroundImage;
            BackgroundImage = ImageSource.FromResource(path);

            //-----------------------------------------------
            // add the obstacles to the GL layer

            // this should already have been done during 
            // level construction
            
            //-----------------------------------------------
            // add the obstacles to the Graphics layer

            foreach(var obstacle in map.Obstacles)
            {
                Tile tile = new Tile(obstacle);

                /*
                                SKImageInfo info = new SKImageInfo(
                                    (int)tile.Rectangle.Width, 
                                    (int)tile.Rectangle.Height);
                 */

                Assembly assembly = GetType().GetTypeInfo().Assembly;

                using (Stream stream = assembly.GetManifestResourceStream(tile.ImageSource))
                {
                    tile.Image = SKBitmap.Decode(stream);
                }

                //tile.Image = SKBitmap.Decode(tile.ImageSource, info);

                DeviceScreen.Tiles.Add(tile);
            }
        }
    }
}
 