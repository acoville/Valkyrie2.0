using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Host;
using NUnit.Framework;
using Valkryie.GL;
using Valkyrie.App.Model;
using Valkyrie.GL;

namespace Valkyrie.Model.Test
{
    public class ActorCollisionTest
    {
        internal GLCharacter glchar1;
        internal Actor actor1;

        internal GLCharacter glchar2;
        internal Actor actor2;

        //=================================================================
        
        [SetUp]
        public void Setup()
        {
            glchar1 = new GLCharacter();
            glchar1.GLRect.TileWidth = 2;
            glchar1.GLRect.TileHeight = 2;
            
            glchar1.GLPosition = new Valkryie.GL.GLPosition(100.0f, 0.0f);
            glchar1.GLRect = new GLRect(glchar1.GLPosition, 64.0f, 64.0f);

            glchar2 = new GLCharacter();
            glchar2.GLRect.TileWidth = 2;
            glchar2.GLRect.TileHeight = 2;
            glchar2.GLPosition = new Valkryie.GL.GLPosition(100.0f, 0.0f);
            glchar1.GLRect = new GLRect(glchar2.GLPosition, 64.0f, 64.0f);

            actor1 = new Actor(glchar1);
            actor2 = new Actor(glchar2);
        }

        //==================================================================

        [Test]
        [Category("Actor")]
        [Category("Collision")]
        [Category("X Axis")]
        public void CollisionTrueTest()
        {
            GLPosition p1 = new GLPosition(100.0f, 0.0f);

            actor1.GLCharacter.MoveTo(p1);
            
            actor2.GLCharacter.MoveTo(p1);

            actor2.GLCharacter.Translate(10.0f, 0.0f, 0.0f);

            Assert.IsTrue(actor1.Intersects(actor2));
        }

        //====================================================================

        [Test]
        [Category("Actor")]
        [Category("Collision")]
        [Category("X Axis")]
        public void CollisionFalseTest()
        {
            GLPosition p1 = new GLPosition(100.0f, 0.0f);
            
            actor1.GLCharacter.MoveTo(p1);
            
            actor2.GLCharacter.MoveTo(p1);

            actor2.GLCharacter.Translate(120.0f, 0.0f, 0.0f);

            Assert.IsFalse(actor1.Intersects(actor2));
        }
    }
}
