using SkiaSharp;
using System.Collections.Generic;
using Valkryie.GL;

namespace Valkyrie.Graphics
{
    public class TileGroup : Drawable
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

            // set the rectangle's depth (Z) here

            float depth = obstacle.Rectangle.Origin.Z;

            for(int i = 0; i < obstacle.Rectangle.TileHeight; i++)
            {
                List<Tile> newRow = new List<Tile>();

                // set the rectangle's top and bottom here

                float top = obstacle.Rectangle.Origin.Y - (i * 64.0f);
                float bottom = obstacle.Rectangle.Origin.Y + (i * 64.0f);

                // set Z here

                float Z = obstacle.Rectangle.Origin.Z;

                //------------------------------------------------------

                int obs_left = (int)obstacle.Rectangle.Left;
                int obs_right = (int)obstacle.Rectangle.Right;
                int limit = (obs_right - obs_left) / 64;

                for(int j = 0; j < limit; j++)
                {
                    // set the rectangle's left and right here 

                    float left = obstacle.Rectangle.Left + (j * 64.0f);

                    float right = left + 64;

                    // create the SKRect, add the tile 

                    SKRect rect = new SKRect(left, top, right, bottom);
                    Tile col = new Tile(obstacle.ImageSource, rect, Z);
                    newRow.Add(col);
                }

                Tiles.Add(newRow);
            }
        }

        //==================================================================

        public override void Translate(float deltaX, float deltaY)
        {
            foreach (var row in Tiles)
            {
                float newY = skiaOrigin_.Y + deltaY;

                for (int i = 0; i < row.Count; i++)
                {
                    float newX = skiaOrigin_.X + deltaX + (i * 64.0f);

                    row[i].Translate(newX, newY);
                }
            }
        }

        //===================================================================

        public override void Move(SKPosition target)
        {
            float deltaZ = target.Z;

            for(int i = 0; i < Tiles.Count; i++)
            {
                for(int j = 0; j < Tiles[i].Count; j++)
                {
                    tiles_[i][j].SKPosition = target;

                    float deltaX = j * 64.0f;
                    float deltaY = i * 64.0f;

                    tiles_[i][j].Translate(deltaX, deltaY);
                    tiles_[i][j].SKPosition.Z = deltaZ;


                    //tiles_[i][j].Translate(deltaX, deltaY, deltaZ);
                }
            }

            skiaOrigin_ = target;
        }
    }
}
