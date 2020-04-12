using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Valkryie.GL;

namespace Valkyrie.GL
{
    public partial class Character
    {
        //=============================================
        // ATTRIBUTES

        internal GLRect gLRect_ = new GLRect();
        public GLRect GLRect
        {
            get => gLRect_;
            set => gLRect_ = value;
        }

        //---------------------------------

        public GLPosition GLPosition
        {
            get => gLRect_.Origin;
            set => gLRect_.MoveTo(value);
        }

        //----------------------------------

        internal string spriteSource_;
        public string SpriteSource
        {
            get => spriteSource_;
            set => spriteSource_ = value;
        }


        //=============================================

        /*----------------------------
         * 
         * Constructors
         * 
         * -------------------------*/

        public Character()
        { }

        //--------------------------------

        public Character(string L)
        {
            Name = L;
        }

        //--------------------------------

        // copy constructor, used during GPVM.AddMonsters()

        public Character(Character orig)
        {
            Name = orig.Name;
            HP = orig.HP;
            MaxHP = orig.MaxHP;

            xSpeed = orig.xSpeed;
            Max_X_Speed = orig.Max_X_Speed;
            xAccelerationRate = orig.xAccelerationRate;

            ySpeed = orig.ySpeed;
        }

        //--------------------------------------------------------

        public Character(XmlNode node)
        {
            //-- who is this character and what team do they belong to? 

            Name = node.Attributes["Name"].Value.ToString();
            Team = int.Parse(node.Attributes["Team"].Value.ToString());

            //-- where does this character go? 

            GLPosition position = new GLPosition();

            position.X = float.Parse(node.Attributes["X"].Value.ToString());
            position.X *= 64.0f;
            
            position.Y = float.Parse(node.Attributes["Y"].Value.ToString());
            position.Y *= 64.0f;

            position.Z = 0.0f;

            GLPosition = position;

            //-- what is the sprite source? 

            SpriteSource = "Valkyrie.App.Images.Characters." 
                + node.Attributes["SpriteSource"].Value.ToString();
        }
    }
}
