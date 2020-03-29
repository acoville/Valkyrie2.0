using System.Xml;

namespace Valkryie.GL
{
    public class GLProp
    {
        //==================================================

        // Properties

        //==================================================

        internal GLPosition position_;
        public GLPosition GLPosition
        {
            get => position_;
            set => position_ = value;
        }

        //--------------------------------------------

        //====================================================

        /*---------------------------------
         *  
         *  This is not acutally ideal, but
         *  the GL Prop is going to carry 
         *  some image information to help
         *  the GPVM build the sprite in the
         *  /Model layer
         * 
         * ------------------------------*/

        internal string imageSource_;
        public string ImageSource
        {
            get => imageSource_;
            set => imageSource_ = value;
        }

        //---------------------------------------

        internal bool scalable_ = false;
        public bool Scalable
        {
            get => scalable_;
            set => scalable_ = value;
        }

        //==================================================

        // FUNCTIONS

        //==================================================

        // constructor

        public GLProp(XmlNode node)
        {
            //--- create the GL position 

            GLPosition origin = new GLPosition();
            origin.X = float.Parse(node.Attributes["X"].Value.ToString());
            origin.X *= 64;

            origin.Y = float.Parse(node.Attributes["Y"].Value.ToString());
            origin.Y *= 64;

            origin.Z = float.Parse(node.Attributes["Z"].Value.ToString());
            origin.Z *= 64;

            position_ = origin;

            //--- extract the image source

            string source = node.Attributes["Image"].Value.ToString();
            imageSource_ = "Valkyrie.App.Images.Props." + source;

            //--- get the scalable information

            Scalable = bool.Parse(node.Attributes["Scalable"].Value.ToString());
        }
    }
}
