using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Valkryie.GL
{
    public class GLPosition
    {
        //================================================

        internal float x_;
        public float X
        {
            get => x_;
            set => x_ = value;
        }

        //================================================

        internal float y_;
        public float Y
        {
            get => y_;
            set => y_ = value;
        }

        //================================================

        /*------------------------------
         * 
         * Constructors
         * 
         * ---------------------------*/

        public GLPosition()
        {}

        //-----------------------------------

        public GLPosition(float Xparam, float Yparam)
        {
            X = Xparam;
            Y = Yparam;
        }

        //----------------------------------

        public GLPosition(XmlNode node)
        {
            X = float.Parse(node.Attributes["X"].Value.ToString());
            Y = float.Parse(node.Attributes["Y"].Value.ToString());
        }

    }
}
