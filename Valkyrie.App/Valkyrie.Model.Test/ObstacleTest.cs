using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using NUnit.Framework;
using Valkryie.GL;
using Valkyrie.App.Model;

namespace Valkyrie.Model.Test
{
    public class ObstacleTest
    {
        internal GLObstacle globs;
        internal Obstacle SUT;

        //========================================================

        [SetUp]
        public void Setup()
        {
            globs = new GLObstacle();
            globs.Rectangle.TileHeight = 1;
            globs.Rectangle.TileWidth = 1;

            GLPosition start = new GLPosition(0.0f, 128.0f);
            globs.MoveTo(start);

            SUT = new Obstacle(globs);
        }

        //=======================================================

        [Test]
        [Category("Obstacle")]
        [Category("Movement")]
        public void MoveTest()
        {
            //SUT.
        }
    }
}
