using SkiaSharp;

namespace Valkyrie.Graphics
{
    public enum Layer { background, middle, foreground };

    public class Drawable : IDrawable
    {
        internal SKBitmap displayImage_;
        public SKBitmap DisplayImage
        {
            get => displayImage_;
            set => displayImage_ = value;
        }

        //===========================================================

        internal SKRect rect_;
        public SKRect Rectangle
        {
            get => rect_;
            set => rect_ = value;
        }

        //===========================================================

        internal SKPosition skiaOrigin_;
        public SKPosition SKPosition
        {
            get => skiaOrigin_;
            set => skiaOrigin_ = value;
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

            SKPosition.Depth = target.Depth;
        }
    }
}
