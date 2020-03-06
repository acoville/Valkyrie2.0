using System;
using System.Collections.Generic;
using System.Text;
using Valkryie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class Obstacle
    {
        internal Position position_;
        public Position Position
        {
            get => position_;
            set => position_ = value;
        }

        //=================================================

        internal GLObstacle obstacle_;
        public GLObstacle GLObs
        {
            get => obstacle_;
            set => obstacle_ = value;
        }

        //================================================

        internal TileGroup tilegroup_;
        public TileGroup Tiles
        {
            get => tilegroup_;
            set => tilegroup_ = value;
        }

        //================================================

        /*-----------------------------------
         * 
         * Constructor
         * 
         * ---------------------------------*/

        public Obstacle(GLObstacle globs)
        {
            obstacle_ = globs;

            Tiles = new TileGroup(obstacle_);

            position_ = new Position(obstacle_.Rectangle.Origin, Tiles.SkiaOrigin);
        }


    }
}
