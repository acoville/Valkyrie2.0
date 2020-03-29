using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Valkryie.GL;

namespace Valkyrie.Graphics
{
    public class Scalable : Drawable
    {
        public Scalable() : base()
        { }

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
            if (DisplayImage != null && SKPosition != null)
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

        //=======================================================================

        public override void Translate(float deltaX, float deltaY, float deltaZ)
        {
            SKPosition origin = SKPosition;

            origin.X += deltaX;
            origin.Y += deltaY;
            origin.Z += deltaZ;

            // add call to scalar here

            Scale();

            // add call to haze filter here

            SKPosition = origin;
            UpdateRectangle();
        }

        //====================================================================

        public override void Move(SKPosition target)
        {
            // target has a Z of 0 here... we lost it somewhere

            SKPosition = target;

            if (SKPosition.Z != target.Z)
            {
                Scale();
            }

            UpdateRectangle();
        }
    }
}
