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

        //==========================================================

        [Test]
        [Category("GL Rectangle")]
        public void TranslateAccuracyTest()
        {
            GLPosition origin1 = new GLPosition(500.0f, 500.0f);
            GLRect SUT = new GLRect(origin1, 3, 3);

            SUT.Translate(50, 0);
            GLPosition origin2 = SUT.Origin;

            GLPosition test = new GLPosition(550.0f, 500.0f);

            Assert.IsTrue(origin2 == test);
        }

        //===========================================================

        [Test]
        [Category("GL Rectangle")]
        public void TranslateCenterTest()
        {
            GLPosition origin1 = new GLPosition(500.0f, 500.0f);
            GLRect SUT = new GLRect(origin1, 3, 3);

            GLPosition center1 = SUT.Center;
            GLPosition test = center1;

            SUT.Translate(50, 0);
            test.Translate(50, 0);

            GLPosition center2 = SUT.Center;

            Assert.IsTrue(center1 == center2);
        }

        //============================================================

        [Test]
        [Category("GL Rectangle")]
        public void ObjectEqualsTrueTest()
        {
            GLRect r1 = new GLRect();
            GLRect r2 = new GLRect();

            Assert.AreEqual(r1, r2);
        }

        //===========================================================

        [Test]
        [Category("GL Rectangle")]
        public void ObjectEqualsFalseTest()
        {
            GLRect r1 = new GLRect(10, 10, 10, 10);
            GLRect r2 = new GLRect(10, 20, 10, 10);

            Assert.AreNotEqual(r1, r2);
        }

        //===========================================================

        [Test]
        [Category("GL Rectangle")]
        public void HashCodeTrueTest()
        {
            GLRect r1 = new GLRect();
            GLRect r2 = new GLRect();

            var hash1 = r1.GetHashCode();
            var hash2 = r2.GetHashCode();

            Assert.AreEqual(hash1, hash2);
        }

        //==========================================================

        [Test]
        [Category("GL Rectangle")]
        public void HashCodeFalseTest()
        {
            GLRect r1 = new GLRect(10, 10, 10, 10);
            GLRect r2 = new GLRect(10, 20, 10, 10);

            var hash1 = r1.GetHashCode();
            var hash2 = r2.GetHashCode();

            Assert.AreNotEqual(r1, r2);
        }

        //============================================================

        [Test]
        [Category("GL Rectangle")]
        public void TranslateBoundsTest()
        {
            GLPosition origin1 = new GLPosition(500.0f, 500.0f);
            GLRect SUT = new GLRect(origin1, 3, 3);

            SUT.Translate(50, 0);


        }

        //===============================================================

        [Test]
        [Category("GL Rectangle")]
        public void EqualityOperatorTrueTest()
        {
            GLRect r1 = new GLRect();
            GLRect r2 = new GLRect();

            Assert.IsTrue(r1 == r2);
        }

        //================================================================

        [Test]
        [Category("GL Rectangle")]
        public void EqualityOperatorFalseTest()
        {
            GLRect r1 = new GLRect(10, 10, 10, 10);
            GLRect r2 = new GLRect(10, 20, 10, 10);

            Assert.IsFalse(r1 == r2);
        }

        //===============================================================

        [Test]
        [Category("GL Rectangle")]
        public void InequalityTrueTest()
        {
            GLRect r1 = new GLRect(10, 10, 10, 10);
            GLRect r2 = new GLRect(10, 20, 10, 10);

            Assert.IsTrue(r1 != r2);
        }

        //==============================================================

        [Test]
        [Category("GL Rectangle")]
        public void InequalityFalseTest()
        {
            GLRect r1 = new GLRect();
            GLRect r2 = new GLRect();

            Assert.IsFalse(r1 != r2);
        }
    }
}
