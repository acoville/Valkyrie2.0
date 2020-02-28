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
        internal GLProp gLProp_;
        public GLProp GLProp
        {
            get => gLProp_;
            set => gLProp_ = value;
        }

        //==================================================

        internal GraphicsProp skiaProp_;
        public GraphicsProp SKProp
        {
            get => skiaProp_;
            set => skiaProp_ = value;
        }

        //==================================================

        // constructors

        public Prop(GLProp prop)
        {
            // gl position

            // layer

            // imagesource


        }
    }
}
