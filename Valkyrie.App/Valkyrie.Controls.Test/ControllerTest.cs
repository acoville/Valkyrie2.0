using NUnit.Framework;
using System.Threading;
using Valkyrie.App.Model;
using Valkyrie.GL;

namespace Valkyrie.Controls.Test
{
    public class ControllerTests
    {
        internal Controller SUT;
        internal GLCharacter character;
        internal Actor actor;

        //====================================================================

        [SetUp]
        public void Setup()
        {
            SUT = new Controller();

            string hadoken = "D, DR, R + B";
            SUT.Commands.Add(hadoken);

            character = new GLCharacter();
            actor = new Actor(character);

            
        }

        //====================================================================

        [Test]
        [Category("Controller")]
        [Category("Actor")]
        public void LinkedControlStateTest()
        {
            actor.ControlStatus = SUT.ControlStatus;
            SUT.ControlStatus.Attack = true;

            var actorIsAttacking = actor.ControlStatus.Attack;

            Assert.IsTrue(actorIsAttacking);

            SUT.ControlStatus.Attack = false;
            actorIsAttacking = actor.ControlStatus.Attack;

            Assert.IsFalse(actorIsAttacking);
        }


        //====================================================================

        /*------------------------------------------------
         * 
         * If the user waits too long (250ms) to enter another
         * button in the sequence, the input string 
         * resets and is overwritten instead of combined
         * 
         * ---------------------------------------------*/

        [Test]
        [Category("Controller")]
        [Category("Input Recognition")]
        public void InputResetTest()
        {
            SUT.Input = "A";
            Thread.Sleep(250);
            
            // have to give it another input since the input string
            // won't reset itself until it gets a new input

            SUT.Input = "A";

            var result = SUT.Input;
            Assert.AreEqual(result, "A");
        }

        //===================================================================

        /*----------------------------------------------------
         * 
         * If the setter property is called within
         * the 250ms time window (1/4 second) then
         * the input string is concatenated instead
         * of overwritten. This still passes at a 249ms delay.
         * 
         * A press is considered simultaneous to another press
         * if it is within 50ms of the previous press
         * 
         * A + B
         * 
         * -------------------------------------------------*/

        [Test]
        [Category("Controller")]
        [Category("Input Recognition")]
        public void SimultaneousInputTest()
        {
            SUT.Input = "A";
            SUT.Input = "B";

            var result = SUT.Input;
            Assert.AreEqual(result, "A + B");
        }

        //=====================================================================

        /*------------------------------------------
         * 
         * A press is considered sequential if 
         * it is within the 250ms window of the last
         * press but outside the 50ms window of a 
         * simultaneous press
         * 
         * A, B
         * 
         * ---------------------------------------*/

        [Test]
        [Category("Controller")]
        [Category("Input Recognition")]
        public void SequentialInputTest()
        {
            SUT.Input = "A";
            Thread.Sleep(200);
            SUT.Input = "B";

            var result = SUT.Input;
            Assert.AreEqual(result, "A, B");
        }

        //=====================================================================

        /*---------------------------------------------------
         * 
         * Test to make sure that we can get a simultaneous 
         * press added to a sequence
         * 
         * D, DR, R + B
         * 
         * ------------------------------------------------*/

        [Test]
        [Category("Controller")]
        [Category("Input Recognition")]
        public void SequentialThenSimultaneousTest()
        {
            SUT.Input = "D";
            Thread.Sleep(100);

            SUT.Input = "DR";
            Thread.Sleep(100);

            SUT.Input = "R";
            Thread.Sleep(10);
            SUT.Input = "B";

            var result = SUT.Input;
            Assert.AreEqual("D, DR, R + B", result);
        }

        //=====================================================================

        [Test]
        [Category("Controller")]
        [Category("Input Recognition")]
        public void ParseCommandTrueTest()
        {
            SUT.Input = "D";
            Thread.Sleep(100);

            SUT.Input = "DR";
            Thread.Sleep(100);

            SUT.Input = "R";
            Thread.Sleep(10);
            SUT.Input = "B";

            Assert.IsTrue(SUT.ParseCommand());
        }

        //======================================================================

        [Test]
        [Category("Controler")]
        [Category("Input Recognition")]
        public void ParseCommandFalseTest()
        {
            SUT.Input = "D";
            Thread.Sleep(100);

            SUT.Input = "DR";
            Thread.Sleep(100);

            SUT.Input = "R";
            Thread.Sleep(10);
            SUT.Input = "A";

            Assert.IsFalse(SUT.ParseCommand());
        }

        //========================================================================

        [Test]
        [Category("Controller")]
        [Category("Control Status")]
        public void JumpTest()
        {
            SUT.ControlStatus.Jump = true;

            //Assert.AreEqual("A", SUT.Input);

            //SUT.Input = "A";
            var result = SUT.ControlStatus.Jump;

            Assert.IsTrue(result);
        }
    }
}