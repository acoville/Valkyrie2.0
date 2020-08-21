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

namespace Valkyrie.Model.Test
{
    public class ActorTest
    {
        internal GLCharacter character;
        internal Sprite sprite;
        
        [SetUp]
        public void Setup()
        {
            character = new GLCharacter();
            sprite = new Sprite();
        }

        //================================================================

        [Test]
        public void SanityCheck()
        {
            Actor SUT = new Actor(character);

            Assert.Pass();
        }

        //===============================================================


        //=========================================================================

        /*--------------------------------------------------
         * 
         * Tests of the X axis acceleration
         * 
         * simulating what GPVM.EvaluateHorizontalMotion()
         * would typically invoke, 
         * 
         * Actor.Translate()
         *      -> GLCharacter.Accelerate()
         * 
         * ------------------------------------------------*/

        [Test]
        [Category("Actor")]
        [Category("Motion")]
        public void Max_X_AccelerationRateTest()
        {
            Actor SUT = new Actor(character);
            SUT.GLCharacter.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.X_Acceleration_Rate = 5.0f;
            SUT.Accelerate();

            float newX = SUT.GLPosition.X;
            Assert.AreEqual(55.0f, newX);
        }
    }
}