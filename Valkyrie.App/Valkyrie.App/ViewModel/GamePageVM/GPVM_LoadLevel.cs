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
        /*-----------------------------------------
         *  
         *  Property to inform the GamePage
         *  weather a level is loaded yet or not 
         * 
         * --------------------------------------*/

        internal bool levelLoaded_ = false;
        public bool LevelLoaded
        {
            get => levelLoaded_;
        }

        //=======================================================================

        /*---------------------------------
         * 
         * 
         * 
         * 
         * ------------------------------*/

        internal void LoadLevel(Level map)
        {
            // set background image

            var path = "Valkyrie.App.Images.Backgrounds." + map.BackgroundImage;
            BackgroundImage = ImageSource.FromResource(path);

            //-- level data

            LoadStartingPosition(map);
            LoadObstacles(map);
            LoadProps(map);
            LoadCharacters(map);

            //-- connect the controller

            actors_[0].ControlStatus = controllers_[0].ControlStatus;
            levelLoaded_ = true;
        }

        //======================================================================

        void LoadStartingPosition(Level map)
        {
            //DeviceScreen.ScrollBox.GLRect.Origin = new GLPosition(map.)
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
                // add to the List of Obstacles in the GPVM from the GLObs in Level

                obstacles_.Add(new Obstacle(glob));

                // get the index of the obstacle we just added

                int i = obstacles_.Count - 1;

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

                obstacles_[i].TilesGroup = new TileGroup(glob);
                obstacles_[i].TilesGroup.MainTile = tileImage;
                obstacles_[i].TilesGroup.EndTile = endImage;
                obstacles_[i].TilesGroup.InitTiles();

                // move it to where it needs to be? 
                // no, the DeviceScreen should do that.

                DeviceScreen.AddObstacle(obstacles_[i]);
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

                // set up control system                

                // is this player1? If yes, then connect it to controllers_[0]

                if (actor.Team == 0)
                {
                    // I think this means any change in controller_[0]'s status
                    // should change the control status of this actor.

                    //actor.ControlStatus = controllers_[0].ControlStatus;

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
 