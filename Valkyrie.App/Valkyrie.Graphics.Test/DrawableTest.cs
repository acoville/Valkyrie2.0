using NUnit.Framework;
using System.Collections.Generic;

namespace Valkyrie.Graphics.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //=========================================================

        [Test]
        [Category("Drawable")]
        public void DrawableSortDescendingOrderTest()
        {
            List<IDrawable> SUT = new List<IDrawable>();

            Drawable d1 = new Drawable();
            d1.Translate(0, 0, 10.0f);

            Drawable d2 = new Drawable();
            d2.Translate(0, 0, 20.0f);
            
            Drawable d3 = new Drawable();
            d3.Translate(0, 0, 30.0f);

            SUT.Add(d1);
            SUT.Add(d2);
            SUT.Add(d3);

            SUT.Sort();

            Drawable first = (Drawable)SUT[0];
            Drawable control = d3;

            Assert.AreEqual(first, control);
        }

        //=====================================================================

        /*--------------------------------------------------------------
         * 
         * Test to make sure that a Drawable.Translate(deltaX, deltaY)
         * and Drawable.Translate(deltaX, deltaY, deltaZ) produce the same
         * XY movement
         * 
         * -----------------------------------------------------------*/

        [Test]
        [Category("Drawable")]
        public void Translate2D_Produces_Same_XY_As_Translate3D()
        {
            Drawable d1 = new Drawable();
            Drawable d2 = new Drawable();

            d1.Translate(25.0f, 25.0f);
            d2.Translate(25.0f, 25.0f, 0.0f);

            Assert.AreEqual(d1.SKPosition, d2.SKPosition);
        }

        //========================================================================


    }
}