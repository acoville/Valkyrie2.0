using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Valkryie.GL;

namespace Valkyrie.Graphics
{
    public class TileGroup
    {
        internal SKBitmap mainTile_;
        public SKBitmap MainTile
        {
            get => mainTile_;

            //--------------------------------------------------
            /*------------------------------
             * 
             * This is where I would set 
             * end-caps on the tile group
             * 
             * ---------------------------*/

            set
            {
                mainTile_ = value;
                foreach(var row in Tiles)
                {
                    foreach(var col in row)
                    {
                        col.Image = value;
                    }
                }
            }
        }

        internal SKBitmap endcapTile_;

        //=====================================================
        
        internal List<List<Tile>> tiles_;
        public List<List<Tile>> Tiles
        {
            get => tiles_;
            set => tiles_ = value;
        }

        //=====================================================

        /*------------------------------------
         * 
         * The Tile Group is how the obstacle
         * will be represented on-screen.
         * 
         * ---------------------------------*/

        public TileGroup(GLObstacle obstacle)
        {
            Tiles = new List<List<Tile>>();

            for(int i = 0; i < obstacle.Rectangle.Height; i++)
            {
                List<Tile> newRow = new List<Tile>();

                for(int j = 0; j < obstacle.Rectangle.Width; j++)
                {
                    SKRect rect = new SKRect();
                    Tile col = new Tile(obstacle.ImageSource, rect);
                    newRow.Add(col);
                }

                Tiles.Add(newRow);
            }
        }

        //=====================================================

        /*--------------------------------
         * 
         * Moving / Displaying this group
         * of tiles
         * 
         * -------------------------------*/

        internal SKPoint SKorigin_;
        public SKPoint SKOrigin
        {
            get => SKorigin_;
            set => SKorigin_ = value;
        }

        //=====================================================

        public void Translate(float deltaX, float deltaY)
        {

        }

        //====================================================

        public void MoveTo(SKPoint skiaPoint)
        {

        }

        //====================================================

        public void MoveTo(GLPosition glPoint)
        {

        }
    }
}
