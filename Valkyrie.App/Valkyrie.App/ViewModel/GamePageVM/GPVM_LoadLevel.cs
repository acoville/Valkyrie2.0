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
            // set background image

            var path = "Valkyrie.App.Images.Backgrounds." + map.BackgroundImage;
            BackgroundImage = ImageSource.FromResource(path);

            //-- level data

            LoadObstacles(map);
            LoadProps(map);
            LoadCharacters(map);
        }

        //======================================================================

        /*------------------------------------
         * 
         * Helper Function to load 
         * all obstacles into the graphics
         * layer
         * 
         * ---------------------------------*/

        internal void LoadObstacles(Level map)
        {
            foreach (var glob in map.Obstacles)
            {
                collider.Add_Obtstacle(new Obstacle(glob));

                // get the SKBitmap for the TileGroup

                SKBitmap tileImage = new SKBitmap();
                SKBitmap endImage = new SKBitmap();
                SKImageInfo info = new SKImageInfo(64, 64);

                Assembly assembly = GetType().GetTypeInfo().Assembly;

                //-- the main tile 

                using (Stream stream = assembly.GetManifestResourceStream(glob.ImageSource + ".tile.png"))
                {
                    tileImage = SKBitmap.Decode(stream);
                }

                //-- the endcap tile

                using (Stream stream = assembly.GetManifestResourceStream(glob.ImageSource + ".end.png"))
                {
                    endImage = SKBitmap.Decode(stream);
                }

                //--------------------------------------------------------

                // create the tile group
                // get the index of the obstacle we just added

                int i = collider.Count - 1;
                var obs = collider[i] as Obstacle;

                obs.TilesGroup = new TileGroup(glob)
                {
                    MainTile = tileImage,
                    EndTile = endImage
                };

                obs.TilesGroup.InitTiles();

                // move it to where it needs to be? 
                // no, the DeviceScreen should do that.

                DeviceScreen.AddObstacle(obs);
            }
        }

        //==============================================================================

        /*-----------------------------------------------
         * 
         * Load Characters
         * 
         * --------------------------------------------*/

        internal void LoadCharacters(Level map)
        { 
            foreach(var character in map.Characters)
            {
                Actor actor = new Actor(character);
                
                actor.Sprite = CreateSprite(actor);
                actors_.Add(actor);
                deviceScreen_.AddActor(actor);

                /*
                // set up control system                

                // is this player1? If yes, then connect it to controllers_[0]
                 */

                if (actor.Team == 0)
                {
                    SetupPlayerOneController();
                }
            }
        }

        //=============================================================================

        internal Sprite CreateSprite(Actor actor)
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            Sprite sprite = new Sprite();

            //-- standing

            try
            {
                var source = actor.ImageSource + ".standing.png";

                using (Stream stream = assembly.GetManifestResourceStream(source))
                {
                    sprite.StandingImage = SKBitmap.Decode(stream);
                }
            }
            catch(FileNotFoundException)
            {}

            //-- update the Game Logic Character object's Rectangle
            //-- now that we know how high and wide this sprite is

            actor.GLCharacter.GLRect.PixelHeight = sprite.DisplayImage.Height;
            actor.GLCharacter.GLRect.PixelWidth = sprite.DisplayImage.Width;
            
            actor.Reset_Uncertainty_Region();

            return sprite;
        }

        //==============================================================================

        /*------------------------------------------------
         * 
         * For the moment, this is going to be all 
         * props in the level, but at runtime I am 
         * planning an optimization that will only
         * load stuff into memory that is nearby
         * 
         * -------------------------------------------*/

        internal void LoadProps(Level map)
        {
            foreach(var glprop in map.Props)
            {
                //-- construct the Drawable object

                SKBitmap image = new SKBitmap();
                Assembly assembly = GetType().GetTypeInfo().Assembly;

                using (Stream stream = assembly.GetManifestResourceStream(glprop.ImageSource))
                {
                    image = SKBitmap.Decode(stream);
                }

                SKImageInfo info = new SKImageInfo(image.Width, image.Height);

                //-- construct the App.Model.Prop object

                Prop prop = new Prop(glprop);
                
                if(glprop.Scalable)
                {
                    Scalable sprite = new Scalable();
                    sprite.DisplayImage = new SKBitmap(info);
                    sprite.DisplayImage = image;
                    prop.SKProp = sprite;
                }
                else
                {
                    Drawable sprite = new Drawable();
                    sprite.DisplayImage = new SKBitmap(info);
                    sprite.DisplayImage = image;
                    prop.SKProp = sprite;
                }
                
                //-- add to the GPVM, device screen

                props_.Add(prop);
                DeviceScreen.AddProp(prop);
            }
        }
    }
}
 