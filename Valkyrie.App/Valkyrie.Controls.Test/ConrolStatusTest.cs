using NUnit.Framework;
using System.Threading;

namespace Valkyrie.Controls.Test
{
    public class ConrolStatusTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        //=============================================================

        [Test]
        [Category("ControlStatus")]
        public void SanityTest()
        {
            ControlStatus SUT = new ControlStatus();
        }

        //==============================================================

        /*----------------------------------------------
         * 
         * Null Direction is invoked anytime
         * all directional buttons are released.
         * 
         * interesting... should I only be setting
         * that particular directional back to false?
         * probably. I would rather make that some kind
         * of property and bind it..
         * 
         * ------------------------------------------*/

        [Test]
        [Category("ControlStatus")]
        public void NullDirectionTest()
        {
            ControlStatus SUT = new ControlStatus();
            SUT.DirectionalStatus.D = true;

            SUT.DirectionalStatus.NullDirection();

            bool DownPressed = SUT.DirectionalStatus.D;

            Assert.IsFalse(DownPressed);
        }
    }
}
