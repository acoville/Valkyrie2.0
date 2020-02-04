
using NUnit.Framework;
using Valkryie.GL;

namespace Valkyrie.GL.Test
{
    public class ObstacleTest
    {
        public void Setup()
        {

        }

        //=======================================================

        [Test]
        [Category("Obstacle")]
        public void ConstructionTest()
        {
            GLObstacle SUT = new GLObstacle();
            Assert.Pass();
        }

        //======================================================


    }
}
