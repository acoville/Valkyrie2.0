using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Valkyrie.App.Model;
using Valkyrie.App.ViewModel;
using Valkyrie.GL;

namespace Valkyrie.App.Test
{
    class GPVM_CollisionTest
    {
        internal GamePageViewModel SUT;
        internal GLCharacter gl_player1;
        internal GLCharacter gl_player2;

        internal Actor player1;
        internal Actor player2;

        [SetUp]
        public void Setup()
        {
            SUT = new GamePageViewModel();

            gl_player1 = new GLCharacter();
            player1 = new Actor(gl_player1);

            gl_player2 = new GLCharacter();
            player2 = new Actor(gl_player2);

            SUT.

        }
    }
}
