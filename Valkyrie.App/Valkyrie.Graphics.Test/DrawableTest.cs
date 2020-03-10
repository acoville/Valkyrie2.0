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
    }
}