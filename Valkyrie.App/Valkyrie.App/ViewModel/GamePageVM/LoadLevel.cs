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
using Valkyrie.App.Model;

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

            foreach(var glob in map.Obstacles)
            {
                // add to the List of Obstacles in the GPVM from the GLObs in Level

                obstacles_.Add(new Obstacle(glob));

                //--------------------------------------------------------

                // get the SKBitmap for the TileGroup

                SKBitmap tileImage = new SKBitmap();
                SKImageInfo info = new SKImageInfo(64, 64);

                Assembly assembly = GetType().GetTypeInfo().Assembly;

                using (Stream stream = assembly.GetManifestResourceStream(glob.ImageSource))
                {
                    tileImage = SKBitmap.Decode(stream);
                }

                //--------------------------------------------------------

                // create the tile group

                TileGroup tilegroup = new TileGroup(glob);
                tilegroup.MainTile = tileImage;

                DeviceScreen.Tiles.Add(tilegroup);
            }
        }
    }
}
 