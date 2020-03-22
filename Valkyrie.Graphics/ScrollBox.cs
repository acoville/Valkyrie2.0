/*=======================================================================
 * 
 * Valkyrie 2.0
 * 
 * The Scrollbox class is responsible for indexing Game Logic 
 * coordinates to SkiaSharp coordinates. 
 * 
 * The hardest part of this is translating the Y coordinates
 * Skiasharp's coordinate origin (0, 0) is in the upper left 
 * corner. X translation works fine, where + goes right and - goes
 * left, but Y is inverted from what I would like, where +Y 
 * goes down and -Y goes up. 
 * 
 * To un-invert this in GameLogic as I would prefer, where the lower
 * left corner (0, height) where height is variable depending on the 
 * state of the native display device.
 * 
 * But it goes further than just inverted Y. To scroll, the camera needs
 * a boundging rectangle, approx 75% or 80% of the screen and if the 
 * camera focus (player 1) strays outside the current GL coordinates 
 * then the GLRect needs to move but the Skia sharp coordinates DO NOT.
 * 
 * SkiaSharp coords: stationary
 * Game Logic coords: change to contain the camera focus in bounds
 * 
 * Additionally, the GL boundaries must prevent scrolling, IE stay 
 * within the level's game logic coordinate boundaries.
 * 
 * ===================================================================*/

using SkiaSharp;
using Valkryie.GL;

namespace Valkyrie.Graphics
{
    public class ScrollBox
    {
        internal ScreenInfo info_;

        //=====================================================

        internal SKRect skiaRect_;
        public SKRect Skia
        {
            get => skiaRect_;
            set => skiaRect_ = value;
        }

        //=====================================================

        internal GLRect glRect_;
        public GLRect GLRect
        {
            get => glRect_;
            set => glRect_ = value;
        }

        //=====================================================

        /*----------------------------
         * 
         * Constructors
         * 
         * --------------------------*/

        public ScrollBox()
        {
            skiaRect_ = new SKRect();
            glRect_ = new GLRect();
        }

        public ScrollBox(ScreenInfo info)
        {
            info_ = info;
            skiaRect_ = new SKRect();
            glRect_ = new GLRect();
            Update(info);          
        }

        //==========================================================================

        /*------------------------------
         * 
         * Update method
         * 
         * -----------------------------*/

        public void Update(ScreenInfo info)
        {
            skiaRect_ = UpdateSKRect(info);

            //GLPosition start = map.Start;

            float top = skiaRect_.Bottom;
            float bottom = skiaRect_.Top;

            float left = skiaRect_.Left;
            float right = skiaRect_.Right;

            glRect_ = new GLRect(top, left, right, bottom);
        }

        //=====================================================================================

        /*------------------------------------
         * 
         * Update the SKRect
         * 
         * ---------------------------------*/

        internal SKRect UpdateSKRect(ScreenInfo info)
        {
            float Left = 0.0f;
            float Top = 0.0f;
            float Right = (float)info.Width;
            float Bottom = (float)info.Height;

            float Height = Bottom;
            float Width = Right;

            /*---------------------------------------
             * 
             * Adjust HeightPercent
             * and WidthPercent to 
             * massage the camera scrollbox bounds
             * 
             * -------------------------------------*/

            SKRect Screen = new SKRect(Left, Top, Right, Bottom);

            // width

            float WidthPercent = .55f;

            float newWidth = Width * WidthPercent;
            float deltaX = Width - newWidth;

            float scrollLeft = deltaX / 2.0f;

            float scrollRight = Width - (deltaX / 2.0f);

            //-------------------------------------------------------

            //-- height

            float HeightPercent = .80f;

            float newHeight = Height * HeightPercent;
            float deltaY = Height - newHeight;

            float scrollTop = deltaY;

            float scrollBottom = Bottom - (deltaY * 1.2f);

            //-- the new box as an SKRect object

            return new SKRect(scrollLeft, scrollTop, scrollRight, scrollBottom);
        }

        //==============================================================

        /*-------------------------------------
         * 
         * Convert a GL position to 
         * a Skia position based on 
         * the current ScreenInfo
         * 
         * I'm not sure I can solve this 
         * without more information.. 
         * is this a block? h = 64 pixels
         * a sprite? h = x pixels
         * 
         * -----------------------------------*/

        public SKPosition ToSkia(GLPosition p)
        {

            // determine X

            float deltaX = GLRect.Origin.X - p.X;
            float skiaX = this.skiaRect_.Left;

            // determine Y

            float GL_Origin_Y = skiaRect_.Bottom;

            float deltaY = p.Y - GLRect.Bottom;
           
            float skiaY = GL_Origin_Y - deltaY;
            

            /*
            float height = skiaRect_.Height;
            float skiaY = this.skiaRect_.Bottom - height;
            float deltaY = p.Y - boxOrigin.Y;

            skiaY -= deltaY;
             */

            // make sure not to drop the Z data

            SKPosition target = new SKPosition(skiaX, skiaY, p.Z);
            target.Translate(-deltaX, deltaY);

            return target;
        }

        //==================================================================

        /*------------------------------------------
         * 
         * Convert a GLRect to a 
         * SKRect based on the current
         * ScreenInfo
         * 
         * Ok, now I need to determine where 
         * the GLRect r is in relation to this
         * ScrollBox's GLRect. This will in 
         * turn provide a starting point to 
         * convert it into a SKRect
         * 
         * what is the current ScreenInfo's 
         * bottom (height)? That is 
         * 
         * --------------------------------------*/

        public SKRect ToSkia(GLRect r)
        {
            float bottom = (float)info_.Height - r.Origin.Y;

            float top = bottom - r.PixelHeight;
            
            float left = r.Left;
            
            float right = r.Right;

            SKRect rect = new SKRect(left, top, right, bottom);
            return rect;
        }
    }
}
