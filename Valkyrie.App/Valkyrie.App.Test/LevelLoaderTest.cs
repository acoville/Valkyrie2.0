/*=============================================================

  Test project to assist with the helper classes used
  in various ViewModels, in this case the level loader

============================================================*/

using NUnit.Framework;
using Valkryie.GL;
using Valkyrie.App.ViewModel;

namespace Valkyrie.App.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //======================================================

        [Test]
        [Category("LevelLoader")]
        public void NumberOfMapsTest()
        {
            LevelLoader loader = new LevelLoader();
            int size = loader.LevelNames.Count;

            Assert.AreEqual(size, 2);
        }

        //===============================================================

        /*---------------------------------------------
         * 
         * So it turns out I was trying to parse
         * a node's attribute .ToString() method, 
         * and forgot to use attribute.Value.ToString().
         * 
         * 
         * -------------------------------------------*/

        [Test]
        [Category("LevelLoader")]
        public void CorrectMapNameTest()
        {
            LevelLoader loader = new LevelLoader();
            string mapName = loader.LevelNames[0];
            string correctName = "TestMap1.xml";

            Assert.AreEqual(mapName, correctName);
        }

        //=================================================================

        [Test]
        [Category("LevelLoader")]
        [Category("Level")]
        public void BackgroundImageTest()
        {
            LevelLoader loader = new LevelLoader();
            Level SUT = loader.LoadFirstLevel();

            string background = SUT.BackgroundImage;
            string correctName = "testBackground.png";

            Assert.AreEqual(background, correctName);
        }

        //===============================================================

        /*----------------------------
         * 
         *  Test to make sure that 
         *  we are loading obstacles
         * 
         * --------------------------*/

        [Test]
        [Category("LevelLoader")]
        [Category("Level")]
        public void ObstaclesLoadedTest()
        {
            LevelLoader loader = new LevelLoader();
            Level SUT = loader.LoadFirstLevel();

            int obstacles = SUT.Obstacles.Count;

            Assert.IsTrue(obstacles >= 1);
        }

        //===============================================================

        /*------------------------------
         * 
         * Make sure that the Obstacle
         * is parsed accurately
         * 
         * -----------------------------*/

        [Test]
        [Category("LevelLoader")]
        [Category("Level")]
        public void ObstacleAccuracyTest()
        {
            LevelLoader loader = new LevelLoader();
            Level SUT = loader.LoadFirstLevel();

            GLObstacle glob = SUT.Obstacles[0];

            int tileHeight = glob.Rectangle.TileHeight;
            int tileWidth = glob.Rectangle.TileWidth;

            Assert.AreEqual(tileHeight, 1);
            Assert.AreEqual(tileWidth, 100);
        }
    }
}