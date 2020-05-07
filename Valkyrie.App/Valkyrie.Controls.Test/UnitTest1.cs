using NUnit.Framework;
using System.Threading;
using Valkyrie.App.Model;
using Valkyrie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.Controls.Test
{
    public class ControllerTests
    {
        internal Controller SUT;

        //====================================================================

        [SetUp]
        public void Setup()
        {
            SUT = new Controller();
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
        [Category("Input")]
        public void InputResetTest()
        {
            SUT.Input = "A";
            Thread.Sleep(250);
            SUT.Input = "A";

            var result = SUT.Input;
            Assert.AreEqual(result, "A");
        }

        //===================================================================

        /*------------------------------------------------
         * 
         * If the setter property is called within
         * the 250ms time window (1/4 second) then
         * the input string is concatenated instead
         * of overwritten.
         * 
         * This still passes at a 249ms delay.
         * 
         * ---------------------------------------------*/

        [Test]
        [Category("Controller")]
        [Category("Input")]
        public void InputConcatenateTest()
        {
            SUT.Input = "A";
            Thread.Sleep(200);
            SUT.Input = "A";

            var result = SUT.Input;
            Assert.AreEqual(result, "AA");
        }

        //=====================================================================


    }
}