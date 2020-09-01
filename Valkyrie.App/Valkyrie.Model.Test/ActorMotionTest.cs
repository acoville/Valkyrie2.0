using NUnit.Framework;
using Valkyrie.App.Model;
using Valkyrie.Controls;
using Valkyrie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.Model.Test
{
    public class ActorMotionTest
    {
        internal Actor Bob;
        internal GLCharacter glBob;
        internal Sprite skBob;
        internal Controller bobController;

        //=========================================================

        [SetUp]
        public void Setup()
        {
            glBob = new GLCharacter();
            skBob = new Sprite();
            bobController = new Controller();

            Bob = new Actor(glBob);
            Bob.Sprite = skBob;
            Bob.ControlStatus = bobController.ControlStatus;
            Bob.GLCharacter.GLPosition = new Valkryie.GL.GLPosition(50.0f, 0.0f);
        }

        //=========================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void Positive_Acceleration_Test()
        {
            var oldX = Bob.GLPosition.X;

            Bob.X_Acceleration_Rate = 5.0f;
            Bob.Accelerate();

            var newX = Bob.GLPosition.X;

            var deltaX = newX - oldX;

            Assert.AreEqual(deltaX, 5.0f);
        }

        //=============================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void Negative_Acceleration_Test()
        {
            var oldX = Bob.GLPosition.X;

            Bob.X_Acceleration_Rate = -5.0f;
            Bob.Accelerate();

            var newX = Bob.GLPosition.X;

            var deltaX = newX - oldX;

            Assert.AreEqual(deltaX, -5.0f);
        }

        //=============================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void Positive_Max_X_Acceleration_Test()
        {
            var oldX = Bob.GLPosition.X;

            Bob.X_Acceleration_Rate = 10.0f;    // this should get truncated to 5.5
            Bob.Accelerate();

            var newX = Bob.GLPosition.X;

            var deltaX = newX - oldX;

            Assert.AreEqual(7.5f, deltaX);
        }

        //================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void Negative_Max_X_Acceleration_Test()
        {
            var oldX = Bob.GLPosition.X;

            Bob.X_Acceleration_Rate = -10.0f; // this should get truncated to -5.5
            Bob.Accelerate();

            var newX = Bob.GLPosition.X;

            var deltaX = newX - oldX;

            Assert.AreEqual(-7.5f, deltaX);
        }

        //=======================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void Deceleration_Facing_Right_Test()
        {

        }

        //========================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void Deceleration_Facing_Left_Test()
        {

        }
    }
}
