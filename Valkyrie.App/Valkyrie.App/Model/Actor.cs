using System;
using System.Collections.Generic;
using System.Text;
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
            Sprite = new Sprite();
        }

    }
}