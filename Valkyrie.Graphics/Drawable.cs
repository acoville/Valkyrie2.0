using SkiaSharp;

namespace Valkyrie.Graphics
{
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
    }
}
