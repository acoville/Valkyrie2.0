/*==========================================================
 * 
 *  Valkyrie v2.0
 *  Game Logic Test Project
 *  Character class unit tests
 *  
 *  author: adam.coville@gmail.com
 *  maintainer: 
 * 
 * ========================================================*/

using NUnit.Framework;
using Valkryie.GL;

namespace Valkyrie.GL.Test
{
    public class CharacterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //==========================================================================

        /*-------------------------------------
         * 
         *  Sanity check to make sure 
         *  the class is still instantiable
         * 
         * -----------------------------------*/

        [Test]
        [Category("Character")]
        public void DefaultConstructorTest()
        {
            GLCharacter SUT = new GLCharacter();
            Assert.Pass();
        }

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
        [Category("Character")]
        [Category("Motion")]
        public void Max_X_AccelerationRateTest()
        {
            GLCharacter SUT = new GLCharacter();
            SUT.GLPosition = new GLPosition(50.0f, 0.0f);

            SUT.X_Acceleration_Rate = 5.0f;
            SUT.Accelerate();

            float newX = SUT.GLPosition.X;
            Assert.AreEqual(55.0f, newX);
        }
    }
}