﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Valkyrie.App.Model;
using Valkyrie.Controls;
using Valkyrie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.Model.Test
{
    public class ActorControlTest
    {
        internal Actor Bob;
        internal GLCharacter glBob;
        internal Sprite skBob;
        internal Controller bobController;

        //=====================================================================

        [SetUp]
        public void Setup()
        {
            glBob = new GLCharacter();
            skBob = new Sprite();
            bobController = new Controller();

            Bob = new Actor(glBob);
            Bob.Sprite = skBob;
            Bob.ControlStatus = bobController.ControlStatus;
            Bob.GLCharacter.GLPosition = new Valkryie.GL.GLPosition(50.0f, 0.0f);
        }

        //======================================================================

        [Test]
        [Category("Actor")]
        [Category("Control")]
        public void Right_Button_Test()
        {
            bobController.ControlStatus.DirectionalStatus.R = true;

            var R = Bob.ControlStatus.DirectionalStatus.R;

            Assert.IsTrue(R);
        }

        //======================================================================

        [Test]
        [Category("Actor")]
        [Category("Control")]
        public void Down_Button_Test()
        {
            bobController.ControlStatus.DirectionalStatus.D = true;

            var D = Bob.ControlStatus.DirectionalStatus.D;

            Assert.IsTrue(D);
        }

        //======================================================================

        [Test]
        [Category("Actor")]
        [Category("Control")]
        public void Modifying_Actor_Also_Modifies_Controller_Test()
        {
            Bob.ControlStatus.Jump = true;

            var jump = bobController.ControlStatus.Jump;

            Assert.IsTrue(jump);
        }

        //=======================================================================

        [Test]
        [Category("Actor")]
        [Category("Control")]
        public void Turning_Left_Faces_Left_Test()
        {
            Bob.Facing = Actor.facing.right;

            //Bob.ControlStatus.DirectionalStatus.L = true;

            Bob.TurnLeft();

            var newFacing = Bob.Facing;

            Assert.AreEqual(Actor.facing.left, newFacing);
        }

        //=======================================================================

        [Test]
        [Category("Actor")]
        [Category("control")] 
        public void Turning_Right_Faces_Right_Test()
        {
            Bob.Facing = Actor.facing.left;

            //Bob.ControlStatus.DirectionalStatus.R = true;

            Bob.TurnRight();

            var newFacing = Bob.Facing;

            Assert.AreEqual(Actor.facing.right, newFacing);
        }
    }
}
