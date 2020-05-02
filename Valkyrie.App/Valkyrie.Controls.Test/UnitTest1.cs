using NUnit.Framework;
using Valkyrie.App.Model;
using Valkyrie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.Controls.Test
{
    public class ControllerTests
    {
        internal Controller testController;

        internal GLCharacter glc;
        internal Actor player1;

        //====================================================================

        [SetUp]
        public void Setup()
        {
            testController = new Controller();

            glc = new GLCharacter();
            player1 = new Actor(glc);

            //player1.

            // perhaps add a GamePageViewModel here?
        }

        //===================================================================

        [Test]
        [Category("Controller")]
        public void Test1()
        {
            Assert.Pass();
        }

        //===================================================================

        /*------------------------------
         * 
         * 
         * ----------------------------*/

        [Test]
        [Category("Conroller")]
        public void AttackTest()
        {
            Assert.Pass();
        }
    }
}