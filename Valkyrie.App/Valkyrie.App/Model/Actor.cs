using System.Xml;
using Valkryie.GL;
using Valkyrie.GL;
using Valkyrie.Graphics;
using Valkyrie.Controls;

namespace Valkyrie.App.Model
{
    public class Actor
    {
        //=====================================================================

        /*---------------------------------------
         * 
         * Control Status
         * 
         * ------------------------------------*/

        internal ControlStatus status_;
        public ControlStatus ControlStatus
        {
            get => status_;
            set => status_ = value;
        }

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

        //-------------------------------------------------

        public int Team
        {
            get => character_.Team;
            set => character_.Team = value;
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

        public void Translate(float deltaX, float deltaY, float deltaZ = 0.0f)
        {
            character_.GLPosition.Translate(deltaX, deltaY, deltaZ);
            Sprite.Translate(deltaX, (-deltaY), deltaZ);
        }
    }
}