using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Valkryie.GL;
using Valkyrie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class Actor
    {
        //=====================================================================

        /*------------------------------------
         * 
         * Valkyrie.Game Logic related state
         * 
         * ----------------------------------*/

        internal GLCharacter character_;
        public GLCharacter GLCharacter
        {
            get => character_;
            set => character_ = value;
        }

        //--------------------------------------------------

        public GLPosition GLPosition
        {
            get => character_.GLPosition;
            //set => character_.GLPosition.MoveTo(value);
        }

        //====================================================================

        /*-----------------------------------
         * 
         * Valkyrie.Graphics related state
         * 
         * --------------------------------*/

        internal Sprite sprite_;
        public Sprite Sprite
        {
            get => sprite_;
            set => sprite_ = value;
        }

        //-------------------------------------------------

        public SKPosition SKPosition
        {
            get => sprite_.SKPosition;
            //set => sprite_.Move(value);
        }

        //--------------------------------------------------

        internal string imageSource_;
        public string ImageSource
        {
            get => imageSource_;
            set => imageSource_ = value;
        }

        //===================================================================

        /*--------------------------------
         * 
         * Valkyrie.Model 
         * 
         * -----------------------------*/

        public Actor(GLCharacter character)
        {
            GLCharacter = character;
            imageSource_ = GLCharacter.SpriteSource;
            Sprite = new Sprite();
        }

        //==================================================================

        public Actor(XmlNode node)
        {
            GLCharacter = new GLCharacter(node);
            imageSource_ = GLCharacter.SpriteSource;
            Sprite = new Sprite();
        }

        //==================================================================

        /*-----------------------------------------
         * 
         * The problem with trying to handle
         * this internally is that I need access
         * to the GPVM DeviceScreen's scrollbox
         * to get an accurate Skia position.
         * 
         * ---------------------------------------*/

        public void MoveTo(GLPosition target)
        {

        }

        //==================================================================

        public void Translate(float deltaX, float deltaY, float deltaZ = 0.0f)
        {
            character_.GLPosition.Translate(deltaX, deltaY, deltaZ);
            Sprite.Translate(deltaX, (-deltaY), deltaZ);
        }

        //===================================================================

        /*---------------------------
         * 
         * Control
         * 

        public Status Status
        {
            get => GLCharacter.
        }
         * ------------------------*/
    }
}