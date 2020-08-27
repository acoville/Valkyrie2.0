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

        //=======================================================================
        
        /*---------------------------------
         * 
         * Equality, Inequality tests
         * because someting is not working
         * right downstream
         * 
         * -------------------------------*/

        [Test]
        [Category("ControlStatus")]
        public void Object_AreEqual_True_Test()
        {
            ControlStatus status1 = new ControlStatus();
            ControlStatus status2 = new ControlStatus();

            Assert.AreEqual(status1, status2);
        }

        //----------------------------------------------------

        [Test]
        [Category("ControlStatus")]
        public void Object_AreEqual_False_Test()
        {
            ControlStatus status1 = new ControlStatus();
            ControlStatus status2 = new ControlStatus();

            status2.Attack = true;

            Assert.AreNotEqual(status1, status2);
        }

        //----------------------------------------------------

        [Test]
        [Category("ControlStatus")]
        public void Equality_Operator_True_Test()
        {
            ControlStatus status1 = new ControlStatus();
            ControlStatus status2 = new ControlStatus();

            Assert.IsTrue(status1 == status2);
        }

        //-----------------------------------------------------

        [Test]
        [Category("ControlStatus")]
        public void Equality_Operator_False_Test()
        {
            ControlStatus status1 = new ControlStatus();
            ControlStatus status2 = new ControlStatus();

            status2.Attack = true;

            Assert.IsFalse(status1 == status2);
        }
    }
}
