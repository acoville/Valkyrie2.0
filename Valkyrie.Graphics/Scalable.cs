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

        public override void Scale()
        {
            if (DisplayImage != null && SKPosition != null)
            {
                //-- get the current display image information 

                float oldHeight = Rectangle.Height;
                float oldWidth = Rectangle.Width;

                //-- get the current SKPosition

                SKPosition oldPosition = this.SKPosition;

                //-- determine the scaled image dimensions

                float scalar = (float)1.0 - (SKPosition.Z / 64 * .1f);

                float newHeight = oldHeight * scalar;
                float newWidth = oldWidth * scalar;

                SKSize newSize = new SKSize(newWidth, newHeight);
                
                //Rectangle.Inflate(newSize);

                SKImageInfo NewInfo = new SKImageInfo((int)newHeight, (int)newWidth);

                //-- perform the Scaling, copy to the DisplayImage

                SKBitmap newDisplayImage = new SKBitmap(NewInfo);
                
                DisplayImage.ScalePixels(newDisplayImage, SKFilterQuality.High);
                
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
            SKPosition = target;

            Scale();

            UpdateRectangle();
        }
    }
}
