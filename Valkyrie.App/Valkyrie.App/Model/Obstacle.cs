using System;
using System.Collections.Generic;
using System.Text;
using Valkryie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class Obstacle
    {
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
         * 
         * 
         * ---------------------------------*/

        public Obstacle(GLObstacle globs)
        {
            obstacle_ = globs;

            Tiles = new TileGroup(obstacle_);


        }


    }
}
