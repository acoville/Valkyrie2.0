using System.Xml;
using Valkryie.GL;

namespace Valkyrie.GL
{
    public partial class GLCharacter
    {
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

        public GLCharacter()
        { }

        //--------------------------------------------------------

        public GLCharacter(XmlNode node)
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

        //=========================================================================

        /*----------------------------------
         * 
         * Updates the GL Position 
         * and the GL Rectangle
         * 
         * --------------------------------*/

        public void Translate(float deltaX, float deltaY, float deltaZ = 0.0f)
        {
            GLPosition.Translate(deltaX, deltaY, deltaZ);
            GLRect.Translate(deltaX, deltaY, deltaZ);
        }

        //=========================================================================

        /*-------------------------------------
         * 
         * MoveTo updates the GLPosition
         * and the GL Rectangle
         * 
         * ----------------------------------*/

        public void MoveTo(GLPosition position)
        {
            GLPosition.MoveTo(position);
            GLRect.MoveTo(position);
        }
    }
}
