/*==========================================================
 * 
 *  Valkyrie v2.0
 *  Game Logic Test Project
 *  Level class unit test suite
 *  
 *  author: adam.coville@gmail.com
 *  maintainer: 
 * 
 * ========================================================*/

using NUnit.Framework;
using Valkryie.GL;

namespace Valkyrie.GL.Test
{
    public class LevelTest
    {
        [SetUp]
        public void Setup()
        {

        }

        //==============================================================

        [Test]
        [Category("Level")]
        public void SanityCheck()
        {
            Level SUT = new Level();
        }
    }
}
