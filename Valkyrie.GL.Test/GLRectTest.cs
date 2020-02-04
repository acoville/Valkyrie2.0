using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Valkryie.GL;

namespace Valkyrie.GL.Test
{
    public class GLRectTest
    {
        public void SetUp()
        {

        }

        //========================================================

        [Test]
        [Category("GL Rectangle")]
        public void PixelsConstructorTest()
        {
            GLPosition origin = new GLPosition(500, 500);
            GLRect SUT = new GLRect(origin, 128.0f, 64.0f);

            Assert.AreEqual(SUT.Top, 628);
            Assert.AreEqual(SUT.Right, 564);
        }

        //=========================================================

        [Test]
        [Category("GL Rectangle")]
        public void BlocksConstructorTest()
        {
            GLPosition origin = new GLPosition(500, 500);
            GLRect SUT = new GLRect(origin, 2, 1);

            Assert.AreEqual(SUT.Top, 628);
            Assert.AreEqual(SUT.Right, 564);
        }

        //=========================================================

        [Test]
        [Category("GL Rectangle")]
        public void CenterAccuracyTest()
        {
            GLPosition origin = new GLPosition(500, 500);
            GLRect SUT = new GLRect(origin, 2, 1);

            float centerX = SUT.Center.X;
            float centerY = SUT.Center.Y;

            Assert.AreEqual(centerY, 564);
            Assert.AreEqual(centerX, 532);
        }

        //=========================================================

        [Test]
        [Category("GL Rectangle")]
        public void ContainsPositionTrueTest()
        {
            GLPosition origin = new GLPosition(500, 500);
            GLRect SUT = new GLRect(origin, 2, 1);

            GLPosition center = SUT.Center;

            Assert.IsTrue(SUT.Contains(center));
        }

        //==========================================================

        [Test]
        [Category("GL Rectangle")]
        public void ContainsPositionFalseTest()
        {
            GLPosition origin = new GLPosition(500, 500);
            GLRect SUT = new GLRect(origin, 2, 1);

            GLPosition testpoint = SUT.Center;
            testpoint.Y += 500.0f;

            Assert.IsFalse(SUT.Contains(testpoint));
        }

        //===========================================================

        [Test]
        [Category("GL Rectangle")]
        public void ContainsRectTrueTest()
        {
            GLPosition origin1 = new GLPosition(500, 500);
            GLRect Rect1 = new GLRect(origin1, 3, 3);
            GLRect Rect2 = new GLRect(origin1, 2, 2);

            Assert.IsTrue(Rect1.Contains(Rect2));
        }

        //===========================================================

        [Test]
        [Category("GL Rectangle")]
        public void ContainsRectFalseTest()
        {
            GLPosition origin1 = new GLPosition(500, 500);
            GLRect Rect1 = new GLRect(origin1, 3, 3);

            GLPosition origin2 = new GLPosition(100, 100);
            GLRect Rect2 = new GLRect(origin2, 2, 2);

            Assert.IsFalse(Rect1.Contains(Rect2));
        }

        //==============================================================

        [Test]
        [Category("GL Rectangle")]
        public void IntersectsTrueTest()
        {
            GLPosition origin1 = new GLPosition(500, 500);
            GLRect Rect1 = new GLRect(origin1, 3, 3);

            GLPosition origin2 = new GLPosition(500, 400);
            GLRect Rect2 = new GLRect(origin2, 4, 4);

            Assert.IsTrue(Rect1.Intersects(Rect2));
        }

        //============================================================

        [Test]
        [Category("GL Rectangle")]
        public void IntersectsFalseTest()
        {
            GLPosition origin1 = new GLPosition(500.0f, 500.0f);
            GLRect Rect1 = new GLRect(origin1, 3, 3);

            GLPosition origin2 = new GLPosition(200.0f, 400.0f);
            GLRect Rect2 = new GLRect(origin2, 1, 1);

            Assert.IsFalse(Rect1.Intersects(Rect2));
        }
    }
}
