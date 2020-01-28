
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
            Obstacle SUT = new Obstacle();
            Assert.Pass();
        }

        //======================================================


    }
}
