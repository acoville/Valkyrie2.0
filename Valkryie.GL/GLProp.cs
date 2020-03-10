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

        internal string imageSource_;
        public string ImageSource
        {
            get => imageSource_;
            set => imageSource_ = value;
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
        }
    }
}
