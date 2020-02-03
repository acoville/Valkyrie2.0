using System;
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
    }
}
