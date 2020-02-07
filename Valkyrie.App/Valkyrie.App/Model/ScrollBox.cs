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
using System;
using System.Collections.Generic;
using System.Text;
using Valkryie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class ScrollBox
    {
        internal SKRect skiaRect_;
        public SKRect Skia
        {
            get => skiaRect_;
        }

        internal GLRect glRect_;

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

        //--------------------------------

        public ScrollBox(ScreenInfo info, ref Level map)
        {
            Update(info, ref map);          
        }

        //==========================================================================

        /*------------------------------
         * 
         * Update method
         * 
         * -----------------------------*/

        public void Update(ScreenInfo info, ref Level map)
        {
            float Left = 0.0f;
            float Top = 0.0f;
            float Right = (float)info.Width;
            float Bottom = (float)info.Height;

            float height = Bottom;
            float width = Right;

            /*---------------------------------------
             * 
             * Let's start with 75% of the screen and
             * go from there.
             * 
             * -------------------------------------*/

            SKRect Screen = new SKRect(Left, Top, Right, Bottom);
            SKPoint center = new SKPoint(Screen.MidX, Screen.MidY);

            float newHeight = height * .75f;
            float deltaY = height - newHeight;

            float newWidth = width * .75f;
            float deltaX = width - newWidth;

            float scrollTop = deltaY / 2;
            float scrollBottom = Bottom - (deltaY / 2);

            float scrollLeft = deltaX / 2.0f;
            float scrollRight = width - (deltaX / 2.0f);

            skiaRect_ = new SKRect(scrollLeft, scrollTop, scrollRight, scrollBottom);

            /*---------------------------------------
             * 
             * 
             * ------------------------------------*/

            //GLPosition start = map.Start;

            /*-----------------------------------------
             * 
             * Somehow have to make sure that I am not 
             * trying to render part of the level 
             * which is outside the level's boundaries.. 
             * 
             * ---------------------------------------*/

        }
    }
}
