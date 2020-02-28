/*==============================================================
 * 
 * Okay so, this class needs to know: 
 *  - where in the GL coordinate system to draw the prop
 *  - what the image source is
 *  - weather to place it in the foreground, background or mid
 *  
 *  This is going to be consructed by the GPVM by examining
 *  the Level's props collection
 * 
 * 
 * ==========================================================*/

using System.Xml;
using Valkryie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class Prop
    {
        internal GLProp glProp_;
        public GLProp GLProp
        {
            get => glProp_;
            set => glProp_ = value;
        }

        //==================================================

        internal Drawable skiaProp_;
        public Drawable SKProp
        {
            get => skiaProp_;
            set => skiaProp_ = value;
        }

        //==================================================

        // constructors

        public Prop(GLProp prop)
        {
            glProp_ = prop;
            skiaProp_ = new GraphicsProp(glProp_);
        }
    }
}
