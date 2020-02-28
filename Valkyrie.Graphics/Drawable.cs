using SkiaSharp;

namespace Valkyrie.Graphics
{
    public enum Layer { background, middle, foreground };

    public class Drawable
    {
        public string ImageSource
        {
            get;
            set;
        }

        //============================================================

        public SKBitmap DisplayImage
        {
            get;
            set;
        }

        //====================================================

        internal Layer layer_ = Layer.middle;
        public Layer RenderLayer
        {
            get => layer_;
            set => layer_ = value;
        }

        //===========================================================

        public SKRect Rectangle
        {
            get;
            set;
        }

        //===========================================================

        internal SKPoint skiaOrigin_;
        public SKPoint SkiaOrigin
        {
            get => skiaOrigin_;
            set => skiaOrigin_ = value;
        }

        //===========================================================

        /*-------------------------------------------
         * 
         * Creates a new temporary SKBitmap which 
         * is the reverse (mirror) of the current
         * display image.
         * 
         * ----------------------------------------*/

        public void Mirror()
        {
            SKBitmap newImage = new SKBitmap(DisplayImage.Width, DisplayImage.Height, false);

            for (int i = 0; i < DisplayImage.Height; i++)
            {
                for (int j = 0; j < DisplayImage.Width; j++)
                {
                    SKColor color = DisplayImage.GetPixel(DisplayImage.Width - j, i);

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
            SKPoint origin = SkiaOrigin;

            origin.X += deltaX;
            origin.Y += deltaY;

            SkiaOrigin = origin;
        }

        //===========================================================

        /*---------------------------
         * 
         * Move
         * moves to a given SK coordinate
         * 
         * -------------------------*/

        public virtual void Move(SKPoint target)
        {
            SkiaOrigin = target;
            float height = Rectangle.Height;
            float width = Rectangle.Width;

            Rectangle = new SKRect(
                SkiaOrigin.X,
                SkiaOrigin.X + width,
                SkiaOrigin.Y,
                SkiaOrigin.Y + height);
        }
    }
}
