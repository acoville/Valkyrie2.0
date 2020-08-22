/*===============================================
 * 
 *  Valkyrie v2.0
 *  Model Test Project
 * 
 *  Actor class test suite
 * 
 * author: adam.coville@gmail.com
 * maintainer: 
 * 
 * ===============================================*/

using NUnit.Framework;
using Valkryie.GL;
using Valkyrie.App.Model;
using Valkyrie.GL;
using Valkyrie.Graphics;
using Valkyrie.Controls;

namespace Valkyrie.Model.Test
{
    public class ActorTest
    {
        internal GLCharacter character;
        internal Sprite sprite;
        internal Actor SUT;
        internal Controller test_controller;

        //=========================================================================
        
        [SetUp]
        public void Setup()
        {
            character = new GLCharacter();
            sprite = new Sprite();
            SUT = new Actor(character);
            test_controller = new Controller();

            //-- perhaps I should have a helper function that links 
            // an actor to a controller? 

            SUT.ControlStatus = test_controller.ControlStatus;
        }

        //=========================================================================

        /*--------------------------------------------------
         * 
         * Tests of the X axis acceleration
         * 
         * simulating what GPVM.EvaluateHorizontalMotion()
         * would typically invoke the acceleration rate setters,
         * then the Accelerate() function, which in turn calls
         * Translate()
         * 
         * ------------------------------------------------*/

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void AccelerationTest_X_Axis()
        {
            SUT.GLCharacter.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.X_Acceleration_Rate = 5.0f;
            SUT.Accelerate();

            float newX = SUT.GLPosition.X;
            Assert.AreEqual(55.0f, newX);
        }

        //===================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void Max_X_Acceleration_Rate_Test()
        {
            SUT.GLCharacter.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.X_Acceleration_Rate = 6.0f;
            float newX = SUT.X_Acceleration_Rate;
            Assert.AreEqual(5.5f, newX);
        }

        //==================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void Min_X_Acceleration_Rate_Test()
        {
            SUT.GLCharacter.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.X_Acceleration_Rate = -5.5f;
            float newX = SUT.X_Acceleration_Rate;
            Assert.AreEqual(0.0f, newX);
        }

        //==================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void StopXAxisMotionTest()
        {
            SUT.GLCharacter.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.X_Acceleration_Rate = 5.5f;
            SUT.Accelerate();

            SUT.StopXAxisMotion();

            var NewXAccelRate = SUT.X_Acceleration_Rate;

            Assert.AreEqual(0.0f, NewXAccelRate);
        }

        //=========================================================================

        /*--------------------------------------------------
         * 
         * Tests of the Y axis acceleration
         * 
         * simulating what GPVM.EvaluateVerticalMotion()
         * would typically invoke the acceleration rate setters,
         * then the Accelerate() function, which in turn calls
         * Translate()
         * 
         * ------------------------------------------------*/

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("Y Axis")]
        public void AccelerationTest_Y_Axis()
        {
            SUT.GLCharacter.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.Y_Acceleration_Rate = 5.0f;
            SUT.Accelerate();

            float newY = SUT.GLPosition.Y;
            Assert.AreEqual(5.0f, newY);
        }

        //===================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("Y Axis")]
        public void Max_Y_Acceleration_Rate_Test()
        {
            SUT.GLCharacter.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.Y_Acceleration_Rate = 6.0f;
            float newY = SUT.Y_Acceleration_Rate;
            Assert.AreEqual(5.5f, newY);
        }

        //==================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("Y Axis")]
        public void Min_Y_Acceleration_Rate_Test()
        {
            SUT.GLCharacter.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.Y_Acceleration_Rate = -5.5f;
            float newY = SUT.Y_Acceleration_Rate;
            Assert.AreEqual(0.0f, newY);
        }

        //==================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("Y Axis")]
        public void StopYAxisMotionTest()
        {
            SUT.GLCharacter.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.Y_Acceleration_Rate = 5.5f;
            SUT.Accelerate();

            SUT.StopYAxisMotion();

            var NewYAccelRate = SUT.Y_Acceleration_Rate;

            Assert.AreEqual(0.0f, NewYAccelRate);
        }

        //=================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        public void FacingRightTest()
        {
            // actor's direction should now be right

            var facing = SUT.Facing;

            Assert.AreEqual(facing, Actor.facing.right);
        }

        //==================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        public void FacingLeftTest()
        {
            SUT.Facing = Actor.facing.left;

            // actor's direction should now be left

            var facing = SUT.Facing;

            Assert.AreEqual(Actor.facing.left, facing);
        }

        //===================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void FacingLeftCausesNegativeDeltaXTest()
        {
            var oldX = SUT.GLPosition.X;

            SUT.Facing = Actor.facing.left;
            SUT.X_Acceleration_Rate = 5.5f;
            SUT.Accelerate();

            var newX = SUT.GLPosition.X;

            var expectedX = oldX - 5.5f;

            Assert.AreEqual(expectedX, newX);
        }

        //===================================================================

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        [Category("X Axis")]
        public void FacingRightCausesPositiveDeltaXTest()
        {
            var oldX = SUT.GLPosition.X;

            SUT.Facing = Actor.facing.right;
            SUT.X_Acceleration_Rate = 5.5f;
            SUT.Accelerate();

            var newX = SUT.GLPosition.X;

            var expectedX = oldX + 5.5f;

            Assert.AreEqual(expectedX, newX);
        }

        

    }
}