using NUnit.Framework;
using Valkryie.GL;
using Valkyrie.App.Model;

namespace Valkyrie.Model.Test
{
    public class EntityTest
    {
        GLRect rect1;   // does not intersect rect2
        GLRect rect2;   // does not intersect rect1
        GLRect rect3;   // contained by rect 1
        GLRect rect4;   // intersects rect1
        GLRect rect5;   // is above rect1
        GLRect rect6;   // is below rect1

        Entity e1;
        Entity e2;
        Entity e3;
        Entity e4;
        Entity e5;
        Entity e6;

        //==========================================================

        [SetUp]
        public void Setup()
        {
            GLPosition p1 = new GLPosition(25.0f, 25.0f);
            rect1 = new GLRect(p1, 100.0f, 100.0f);
            e1 = new Entity();
            e1.Rectangle = rect1;

            GLPosition p2 = new GLPosition(225.0f, 25.0f);
            rect2 = new GLRect(p2, 100.0f, 100.0f);
            e2 = new Entity();
            e2.Rectangle = rect2;

            GLPosition p3 = new GLPosition(50.0f, 50.0f);
            rect3 = new GLRect(p3, 25.0f, 25.0f);
            e3 = new Entity();
            e3.Rectangle = rect3;

            GLPosition p4 = new GLPosition(-20.0f, 80.0f);
            rect4 = new GLRect(p4, 50.0f, 20.0f);
            e4 = new Entity();
            e4.Rectangle = rect4;


            GLPosition p5 = new GLPosition(75.0f, 175.0f);
            rect5 = new GLRect(p5, 25.0f, 25.0f);
            e5 = new Entity();
            e5.Rectangle = rect5;

            GLPosition p6 = new GLPosition(75.0f, -50.0f);
            rect6 = new GLRect(p6, 25.0f, 25.0f);
            e6 = new Entity();
            e6.Rectangle = rect6;
        }

        //==========================================================

        [Test]
        [Category("Entity")]
        public void Contains_True_Test()
        {
            Assert.IsTrue(e1.Contains(e3));
        }

        //==========================================================

        [Test]
        [Category("Entity")]
        public void Contains_False_Test()
        {
            Assert.IsFalse(e1.Contains(e2));
        }

        //==========================================================

        [Test]
        [Category("Entity")]
        public void Intersects_True_Test()
        {
            Assert.IsTrue(e4.Intersects(e1));
        }

        //==========================================================

        [Test]
        [Category("Entity")]
        public void Intersects_False_Test()
        {
            Assert.IsFalse(e1.Intersects(e2));
        }

        //============================================================

        [Test]
        [Category("Entity")]
        public void Is_Above_True_Test()
        {
            Assert.IsTrue(e5.Is_Above(e1));
        }

        //============================================================

        [Test]
        [Category("Entity")]
        public void Is_Above_False_Test()
        {
            Assert.IsFalse(e6.Is_Above(e1));
        }

        //============================================================

        [Test]
        [Category("Entity")]
        public void Is_Below_True_Test()
        {
            Assert.IsTrue(e6.Is_Below(e1));
        }

        //============================================================

        [Test]
        [Category("Entity")]
        public void Is_Below_False_Test()
        {
            Assert.IsFalse(e5.Is_Below(e6));
        }

        //============================================================

        [Test]
        [Category("Entity")]
        public void Is_Left_True_Test()
        {
            Assert.IsTrue(e1.Is_Left_Of(e2));
        }

        //============================================================

        [Test]
        [Category("Entity")]
        public void Is_Left_False_Test()
        {
            Assert.IsFalse(e2.Is_Left_Of(e1));
        }

        //=============================================================

        [Test]
        [Category("Entity")]
        public void Is_Right_True_Test()
        {
            Assert.IsTrue(e2.Is_Right_Of(e1));
        }

        //============================================================

        [Test]
        [Category("Entity")]
        public void Is_Right_False_Test()
        {
            Assert.IsFalse(e1.Is_Right_Of(e2));
        }
    }
}
