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


    }
}