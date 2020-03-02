using System.Xml;

namespace Valkryie.GL
{
    public class GLProp
    {
        internal GLPosition position_;
        public GLPosition GLPosition
        {
            get => position_;
            set => position_ = value;
        }

        //============================================================

        internal string imageSource_;
        public string ImageSource
        {
            get => imageSource_;
            set => imageSource_ = value;
        }

        //===========================================================

        internal string layer_;
        public string Layer
        {
            get => layer_;
            set => layer_ = value;
        }

        //===========================================================

        public GLProp(XmlNode node)
        {
            //--- create the GL position 

            GLPosition origin = new GLPosition();
            origin.X = float.Parse(node.Attributes["X"].Value.ToString());
            origin.X *= 64;

            origin.Y = float.Parse(node.Attributes["Y"].Value.ToString());
            origin.Y *= 64;

            position_ = origin;

            //--- extract the image source

            string source = node.Attributes["Image"].Value.ToString();
            imageSource_ = "Valkyrie.App.Images.Props." + source;

            //--- what layer is this supposed to be in? 

            layer_ = node.Attributes["Layer"].Value.ToString();
        }
    }
}
