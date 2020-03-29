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

using SkiaSharp;
using Valkryie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class Prop
    {
        //==================================================

        // Properties

        //==================================================

        public GLPosition GLPosition
        {
            get => GLProp.GLPosition;
            set => GLProp.GLPosition = value;
        }
        
        //--------------------------------------------

        public SKPosition SKPosition
        {
            get => SKProp.SKPosition;
            set => SKProp.SKPosition = value;
        }

        //--------------------------------------------

        protected GLProp glProp_;
        public GLProp GLProp
        {
            get => glProp_;
            set => glProp_ = value;
        }

        //--------------------------------------------

        internal IDrawable skiaProp_;
        public IDrawable SKProp
        {
            get => skiaProp_;
            set => skiaProp_ = value;
        }

        //==================================================

        // FUNCTIONS

        //==================================================

        public void MoveSprite(SKPosition target)
        {
            SKProp.Move(target);
        }

        //==================================================

        // constructors

        public Prop(GLProp prop)
        {
            glProp_ = prop;

            skiaProp_ = new Drawable();
            
            skiaProp_.SKPosition.Z = prop.GLPosition.Z;
        }
    }
}
