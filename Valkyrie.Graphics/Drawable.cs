using SkiaSharp;
using System;

namespace Valkyrie.Graphics
{
    public class Drawable : IDrawable, IComparable
    {
        //=============================================
        
        // PROPERTIES
        
        //=============================================

        internal SKBitmap displayImage_;
        public SKBitmap DisplayImage
        {
            get => displayImage_;
            set => displayImage_ = value;
        }

        //-------------------------------------

        internal SKRect rect_;
        public SKRect Rectangle
        {
            get => rect_;
            set => rect_ = value;
        }

        //-------------------------------------

        internal SKPosition skiaOrigin_;
        public SKPosition SKPosition
        {
            get => skiaOrigin_;
            set => skiaOrigin_ = value;
        }
        
        //===========================================================

        // FUNCTIONS

        //===========================================================

        public Drawable()
        {
            SKPosition = new SKPosition(0, 0, 0);
            DisplayImage = new SKBitmap();
        }

        //===========================================================

        /*------------------------------------------------
         * 
         * Creates a new temporary SKBitmap which 
         * is the reverse (mirror) of the current
         * display image.
         * 
         * I'm generating unwanted artifacts in 
         * a tile where the first few pixels are empty
         * not sure why. 
         * 
         * ---------------------------------------------*/

        public void Mirror()
        {
            SKBitmap newImage = new SKBitmap(DisplayImage.Width, DisplayImage.Height, false);

            for (int i = 0; i < DisplayImage.Height; i++)
            {
                for (int j = 0; j < DisplayImage.Width; j++)
                {
                    SKColor color = DisplayImage.GetPixel(DisplayImage.Width - j, i);

                    // check to see if the pixel is empty

                    int r = color.Red;
                    int b = color.Blue;
                    int g = color.Green;

                    if(r != 0 && b != 0 && g != 0)
                        newImage.SetPixel(j, i, color);
                }
            }

            DisplayImage = newImage;
        }

        //========================================================

        /*----------------------------
         * 
         * Translate
         * moves the tile by an 
         * X and Y offset
         * 
         * --------------------------*/

        public virtual void Translate(float deltaX, float deltaY)
        {
            SKPoint origin = SKPosition.SKPoint;

            origin.X += deltaX;
            origin.Y += deltaY;

            SKPosition = origin;
        }

        //===========================================================

        public virtual void Translate(float deltaX, float deltaY, float deltaZ)
        {
            SKPosition origin = SKPosition;
            origin.X += deltaX;
            origin.Y += deltaY;
            
            origin.Depth += deltaZ;

            // add call to scalar here

            // add call to haze filter here

            SKPosition = origin;
        }

        //===========================================================

        /*----------------------------------
         * 
         * Move
         * moves to a given SK coordinate
         * 
         * ------------------------------*/

        public virtual void Move(SKPosition target)
        {
            SKPosition = target;

            float height = Rectangle.Height;
            float width = Rectangle.Width;

            Rectangle = new SKRect(
                SKPosition.X,
                SKPosition.X + width,
                SKPosition.Y,
                SKPosition.Y + height);

            if(SKPosition.Depth != target.Depth)
            {
                float deltaZ = SKPosition.Depth - target.Depth;
                SKPosition.Depth = target.Depth;      
            }
        }

        //============================================================

        /*------------------------------------
         * 
         *  Default Comparison of Drawables
         *  will be to compare depth in 
         *  descending order
         * 
         * ---------------------------------*/

        int IComparable.CompareTo(object obj)
        {
            var other = (Drawable)obj;

            return DepthCompare(other);
        }

        //==========================================================

        internal int DepthCompare(Drawable other)
        {
            if(SKPosition.Depth > other.SKPosition.Depth)
            {
                return -1;
            }
            
            else if(SKPosition.Depth == other.SKPosition.Depth)
            {
                return 0;
            }

            else
            {
                return 1;
            }
        }

        //===========================================================

        /*-----------------------------------------
         * 
         * property that controls foreshortening
         * depening on the depth of the sprite
         * 
         * --------------------------------------*/

        internal float scalar_ = 1.0f;
        public float Scalar
        {
            get => scalar_;
            
            set
            {
                scalar_ = value;
            }
        }

        //=========================================================

        /*------------------------------------------------
        * 
        * goals of this function:
        * 
        * keep the "Bottom" unchanged, only
        * shorten it or grow it from the top down
        * 
        * Keep horizontal growht or contraction centered
        * 
        * ----------------------------------------------*/

        public virtual void Scale()
        {
            if(DisplayImage != null && SKPosition != null)
            {
                float oldBottom = Rectangle.Bottom;

                float oldHeight = Math.Abs(Rectangle.Height);
                float oldWidth = Math.Abs(Rectangle.Width);

                /*
                 * Each prop is being scaled differently, OR 
                 * is being scaled more than once. I literally 
                 * cannot solve this.
                 */

                if(oldHeight <= 0 || oldWidth <= 0)
                {
                    return;
                }

                float scalar = 1.0f;

                float newHeight = oldHeight * scalar;
                float newWidth = oldWidth * scalar;

                SKImageInfo info = new SKImageInfo((int)newHeight, (int)newWidth);
                SKBitmap newDisplayImage = new SKBitmap(info);

                DisplayImage.ScalePixels(newDisplayImage, SKFilterQuality.High);
                newDisplayImage.CopyTo(DisplayImage);

                // because +Y is down in Skia, the delta is going to be 
                // new - old, which if we were scaling down would result in 
                // a negative number

                float newBottom = Rectangle.Bottom;

                float deltaY = newBottom - oldBottom;
                Translate(0, deltaY);
            }
        }


    }
}
