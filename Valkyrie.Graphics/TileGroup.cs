using SkiaSharp;
using System.Collections.Generic;
using Valkryie.GL;

namespace Valkyrie.Graphics
{
    public class TileGroup
    {
        internal SKBitmap mainTile_;
        public SKBitmap MainTile
        {
            get => mainTile_;

            set
            {
                mainTile_ = value;
            }
        }

        //========================================================================

        /*------------------------------
         * 
         * Initialize Tiles
         * 
         * ---------------------------*/

        public void InitTiles()
        {
            foreach (var row in Tiles)
            {

                for (int i = 0; i < row.Count; i++)
                {
                    // left endcap

                    if (i == 0)
                    {
                        row[i].DisplayImage = EndTile;
                        row[i].Mirror();
                    }

                    // middle tiles

                    else if (i > 0 && i < row.Count - 1)
                    {
                        row[i].DisplayImage = MainTile;
                    }

                    // right endcap

                    else
                    {
                        row[i].DisplayImage = EndTile;
                    }
                }
            }
        }

        //========================================================================

        internal SKBitmap endTile_;
        public SKBitmap EndTile
        {
            get => endTile_;
            
            set
            {
                endTile_ = value;
                InitTiles();
            }
        }

        //=========================================================================
        
        internal List<List<Tile>> tiles_;
        public List<List<Tile>> Tiles
        {
            get => tiles_;
            set => tiles_ = value;
        }

        //=======================================================================

        /*------------------------------------
         * 
         * The Tile Group is how the obstacle
         * will be represented on-screen.
         * 
         * ---------------------------------*/

        public TileGroup(GLObstacle obstacle)
        {
            Tiles = new List<List<Tile>>();

            for(int i = 0; i < obstacle.Rectangle.TileHeight; i++)
            {
                List<Tile> newRow = new List<Tile>();

                // set the rectangle's top and bottom here

                float top = obstacle.Rectangle.Origin.Y - (i * 64.0f);
                float bottom = obstacle.Rectangle.Origin.Y + (i * 64.0f);

                //------------------------------------------------------

                for(int j = 0; j < obstacle.Rectangle.TileWidth; j++)
                {
                    // set the rectangle's left and right here 

                    float left = obstacle.Rectangle.Origin.X + (j * 64.0f);
                    float right = obstacle.Rectangle.Origin.X - (j * 64.0f);

                    // create the SKRect, add the tile 

                    SKRect rect = new SKRect(left, top, right, bottom);

                    Tile col = new Tile(obstacle.ImageSource, rect);
                    newRow.Add(col);
                }

                Tiles.Add(newRow);
            }
        }

        //====================================================================

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

        //==================================================================

        public void Translate(float deltaX, float deltaY)
        {
            foreach (var row in Tiles)
            {
                foreach (var col in row)
                {
                    col.Translate(deltaX, deltaY);
                }
            }
        }

        //===================================================================

        public void MoveTo(SKPoint skiaPoint)
        {


        }
    }
}
