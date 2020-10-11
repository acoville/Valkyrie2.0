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

            set
            {
                displayImage_ = value;
                UpdateRectangle();
            }
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
            Rectangle = new SKRect();
        }

        //===========================================================

        /*----------------------------------------
         * 
         * Helper function to update the 
         * Drawable's SKRect. I completely 
         * forgot about initializing and updating
         * this even though I was keying Scale() 
         * to it. Ye Gods this cost me a lot of 
         * time and pain. Uninitialized data
         * WILL bite you.
         * 
         * -------------------------------------*/

        internal void UpdateRectangle()
        {
            float width = DisplayImage.Width;
            float height = DisplayImage.Height;

            //-- initialize the rectangle

            float X1 = SKPosition.X;
            float X2 = X1 + width;

            float Y1 = SKPosition.Y;
            float Y2 = Y1 + height;

            Rectangle = new SKRect(X1, Y1, X2, Y2);
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
            float depth = SKPosition.Z;

            origin.X += deltaX;
            origin.Y += deltaY;
            
            SKPosition = origin;
            SKPosition.Z = depth;

            UpdateRectangle();
        }

        //===========================================================

        public virtual void Translate(float deltaX, float deltaY, float deltaZ)
        {
            SKPosition origin = SKPosition;
            
            origin.X += deltaX;
            origin.Y += deltaY;
            origin.Z += deltaZ;

            // add call to scalar here

            //Scale();

            // add call to haze filter here

            SKPosition = origin;
            UpdateRectangle();
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
            // target has a Z of 0 here... we lost it somewhere

            SKPosition = target;

//            if(SKPosition.Z != target.Z)
//            {
//                Scale();      
//            }

            UpdateRectangle();
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
            if(SKPosition.Z > other.SKPosition.Z)
            {
                return -1;
            }
            
            else if(SKPosition.Z == other.SKPosition.Z)
            {
                return 0;
            }

            else
            {
                return 1;
            }
        }

        //=========================================================

        /*-----------------------------------------------------
        * 
        * Scale method
        * 
        * creates a new SKBitmap and scales up the current
        * display image depending on the depth / Z coordinate
        * of the current SKPosition
        * 
        * ----------------------------------------------------*/

        public virtual void Scale()
        {
            if(DisplayImage != null && SKPosition != null)
            {
                //-- get the current display image information 

                float oldHeight = Rectangle.Height;
                float oldWidth = Rectangle.Width;

                //-- determine the scaled image dimensions

                float scalar = (float)1.0 - (SKPosition.Z / 64 * .1f);
                
                float newHeight = oldHeight * scalar;
                float newWidth = oldWidth * scalar;

                SKImageInfo NewInfo = new SKImageInfo((int)newHeight, (int)newWidth);
                
                //-- perform the Scaling, copy to the DisplayImage

                SKBitmap newDisplayImage = new SKBitmap(NewInfo);
                DisplayImage.ScalePixels(newDisplayImage, SKFilterQuality.High);                
                
                UpdateRectangle();
                
                newDisplayImage.CopyTo(DisplayImage);
            }
        }

        //===============================================================================

        /*----------------------------------
         * 
         * Filter Method
         * 
         * ---------------------------------*/

        public void Filter(SKColor maskColor)
        {
            // depending on the depth, we will add more or less of this mask

            byte mR = maskColor.Red;
            byte mG = maskColor.Green;
            byte mB = maskColor.Blue;
            byte mA = maskColor.Alpha;

            for(int y = 0; y < DisplayImage.Height; y++)
            {
                for(int x = 0; x < DisplayImage.Width; x++)
                {
                    SKColor color = new SKColor();
                    color = DisplayImage.GetPixel(x, y);

                    byte R = color.Red;
                    byte G = color.Green;
                    byte B = color.Blue;
                    byte Alpha = color.Alpha;

                    if(R != 0 && G != 0 && B != 0)
                    {
                        R += mR;
                        G += mG;
                        B += mB;
                        Alpha += mA;

                        SKColor result = new SKColor(R, G, B, Alpha);

                        DisplayImage.SetPixel(x, y, result);
                    }
                }
            }
        }

        //================================================================================

        public void Filter(byte R, byte G, byte B, byte Alpha)
        {
            SKColor color = new SKColor(R, G, B, Alpha);
            Filter(color);
        }
    }
}
