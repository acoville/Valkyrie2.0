﻿/*================================================================
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

        //============================================================

        internal void LoadLevel(Level map)
        {
            // set background image

            var path = "Valkyrie.App.Images.Backgrounds." + map.BackgroundImage;
            BackgroundImage = ImageSource.FromResource(path);

            // load obstacles into graphics layer

            LoadObstacles(map);
            
            // set up player1



            // load actors
            

            
            levelLoaded_ = true;
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

                obstacles_[i].Tiles = new TileGroup(glob);
                obstacles_[i].Tiles.MainTile = tileImage;
                obstacles_[i].Tiles.EndTile = endImage;
                obstacles_[i].Tiles.InitTiles();

                // move it to where it needs to be? 
                // no, the DeviceScreen should do that.

                DeviceScreen.Tiles.Add(obstacles_[i].Tiles);
            }
        }

        //==============================================================================


    }
}
 