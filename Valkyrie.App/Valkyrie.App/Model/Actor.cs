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
        //Character character_;

        internal Character character_;
        public Character GLCharacter
        {
            get => character_;
            set => character_ = value;
        }

        //--------------------------------------------------

        public GLPosition GLPosition
        {
            get => character_.GLPosition;
            set => character_.GLPosition.MoveTo(value);
        }

        //-------------------------------------------------

        public SKPosition SKPosition
        {
            get => sprite_.SKPosition;
            set => sprite_.Move(value);
        }

        //--------------------------------------------------

        internal string imageSource_;
        public string ImageSource
        {
            get => imageSource_;
            set => imageSource_ = value;
        }

        //--------------------------------------------------

        //Sprite sprite_;

        internal Sprite sprite_;
        public Sprite Sprite
        {
            get => sprite_;
            set => sprite_ = value;
        }

        //===================================================================

        public Actor(Character character)
        {
            GLCharacter = character;
            imageSource_ = GLCharacter.SpriteSource;
            Sprite = new Sprite();
        }

        //==================================================================

        public Actor(XmlNode node)
        {
            GLCharacter = new Character(node);
            imageSource_ = GLCharacter.SpriteSource;
            Sprite = new Sprite();
        }
    }
}