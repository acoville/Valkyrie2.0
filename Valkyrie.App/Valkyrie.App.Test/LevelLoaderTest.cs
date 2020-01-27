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
        public void LevelLoaderReadsCorrectNumberOfMaps()
        {
            LevelLoader loader = new LevelLoader();
            int size = loader.LevelNames.Count;

            Assert.AreEqual(size, 1);
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
        public void LevelLoaderGetsCorrectMapName()
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
        public void XmlConstructorGetsBackgroundImage()
        {
            LevelLoader loader = new LevelLoader();
            Level SUT = loader.LoadFirstLevel();

            string background = SUT.ImageSource;
            string correctName = "testBackground.png";

            Assert.AreEqual(background, correctName);
        }
    }
}