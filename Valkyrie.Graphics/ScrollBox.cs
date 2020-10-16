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
        internal GLPosition referencePoint;

        internal ScreenInfo info_;

        //=====================================================

        internal SKRect skiaRect_ = new SKRect();
        public SKRect Skia
        {
            get => skiaRect_;
            set => skiaRect_ = value;
        }

        //=====================================================

        internal GLRect glRect_ = new GLRect();
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

        public ScrollBox(ScreenInfo info)
        {
            info_ = info;
            Update(info);          
        }

        //========================================================

        public ScrollBox(ScreenInfo info, GLPosition start)
        {
            info_ = info;

        }

        //==========================================================================

        /*------------------------------
         * 
         * Update method
         * 
         * -----------------------------*/

        public void Update(ScreenInfo info)
        {
            UpdateSKRect(info);
            UpdateGLRect(info_, info);

            /*

            float top = skiaRect_.Bottom;
            float bottom = skiaRect_.Top;

            float left = skiaRect_.Left;
            float right = skiaRect_.Right;

            glRect_ = new GLRect(top, left, right, bottom);
             */
        }

        //===========================================================================

        /*------------------------------------
         * 
         * Update the SKRect
         * 
         * ---------------------------------*/

        internal void UpdateSKRect(ScreenInfo info)
        {
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

            this.skiaRect_ = new SKRect(scrollLeft, scrollTop, scrollRight, scrollBottom);
        }

        //===========================================================================

        internal GLRect UpdateGLRect(ScreenInfo info1, ScreenInfo info2)
        {
            GLRect newRect = new GLRect();

            float top = skiaRect_.Bottom;
            float bottom = skiaRect_.Top;

            float left = skiaRect_.Left;
            float right = skiaRect_.Right;

            glRect_ = new GLRect(top, left, right, bottom);


            return newRect;
        }

        //===========================================================================

        /*------------------------------------------
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
         * ---------------------------------------*/

        public SKPosition ToSkia(GLPosition p)
        {
            // determine X

            float deltaX = this.GLRect.Origin.X - p.X;
            float skiaX = this.skiaRect_.Left - deltaX;

            // determine Y

            float GL_Origin_Y = skiaRect_.Bottom;
            float deltaY = p.Y - GLRect.Bottom;
            float skiaY = GL_Origin_Y - deltaY;
  
            // make sure not to drop the Z data

            SKPosition target = new SKPosition(skiaX, skiaY, p.Z);
            
            return target;
        }

        //===========================================================================

        /*----------------------------------------------
         * 
         * I can solve a lot of problems if I just 
         * figure out how to get the GL coordinates of
         * any given Skia coordinate
         *  
         * --------------------------------------------*/

        public GLPosition ToGL(SKPosition skia)
        {
            GLPosition target = new GLPosition();
            GLPosition origin = GLRect.Origin;

            // determine X 

            target.X = skia.X;

            // determine Y 

            target.Y = skia.Y;


            // make sure not to drop the Z data

            target.Z = skia.Z;

            return target;
        }
    }
}
