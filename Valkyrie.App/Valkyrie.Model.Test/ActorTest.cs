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
    }
}