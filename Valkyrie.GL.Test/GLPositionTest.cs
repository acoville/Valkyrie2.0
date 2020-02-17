﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NUnit.Framework;
using Valkryie.GL;

namespace Valkyrie.GL.Test
{
    public class GLPositionTest
    {
        public void SetUp()
        {

        }

        //====================================================

        [Test]
        [Category("GL Position")]
        public void DefaultConstructorTest()
        {
            var SUT = new GLPosition();
            Assert.Pass();
        }

        //==================================================

        [Test]
        [Category("GL Position")]
        public void TranslateAccuracyTest()
        {
            var SUT = new GLPosition(25, 25);

            SUT.Translate(5, -5);

            Assert.AreEqual(30, SUT.X);
            Assert.AreEqual(20, SUT.Y);
        }

        //==============================================

        [Test]
        [Category("GL Position")]
        public void EqualityOperatorTrueTest()
        {
            var p1 = new GLPosition();
            var p2 = new GLPosition();

            Assert.IsTrue(p1 == p2);
        }

        //================================================

        [Test]
        [Category("GL Position")]
        public void EqualityOperatorFalseTest()
        {
            var p1 = new GLPosition(25, 25);
            var p2 = new GLPosition(25, 20);

            Assert.IsFalse(p1 == p2);
        }

        //===============================================

        [Test]
        [Category("GL Position")]
        public void InequalityOperatorTrueTest()
        {
            var p1 = new GLPosition(25, 25);
            var p2 = new GLPosition(25, 20);

            Assert.IsTrue(p1 != p2);
        }

        //===============================================

        [Test]
        [Category("GL Position")]
        public void InequalityOperatorFalseTest()
        {
            var p1 = new GLPosition();
            var p2 = new GLPosition();

            Assert.IsFalse(p1 != p2);
        }

        //===============================================

        [Test]
        [Category("GL Position")]
        public void MoveAccuracyTest()
        {
            var SUT = new GLPosition();
            var destination = new GLPosition(100, 100);

            SUT.MoveTo(destination);

            Assert.IsTrue(SUT == destination);
        }

        //=================================================

        [Test]
        [Category("GL Position")]
        public void HashTrueTest()
        {
            var p1 = new GLPosition();
            var p2 = new GLPosition();

            var hash1 = p1.GetHashCode();
            var hash2 = p2.GetHashCode();

            Assert.AreEqual(hash1, hash2);
        }
        

        //====================================================

        [Test]
        [Category("GL Position")]
        public void HashFalseTest()
        {
            var p1 = new GLPosition(25, 25);
            var p2 = new GLPosition(25, 10);

            var hash1 = p1.GetHashCode();
            var hash2 = p2.GetHashCode();

            Assert.AreNotEqual(hash1, hash2);
        }

        //=====================================================

        [Test]
        [Category("GL Position")]
        public void ObjectEqualsTrue()
        {
            var p1 = new GLPosition();
            var p2 = new GLPosition();

            Assert.AreEqual(p1, p2);
        }

        //=========================================================

        [Test]
        [Category("GL Position")]
        public void ObjectEqualsFalse()
        {
            var p1 = new GLPosition(25, 25);
            var p2 = new GLPosition(25, 10);

            Assert.AreNotEqual(p1, p2);
        }
    }
}
