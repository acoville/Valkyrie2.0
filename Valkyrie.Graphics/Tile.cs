using SkiaSharp;
using Valkryie.GL;

namespace Valkyrie.Graphics
{
    public class Tile
    {

        public Tile(string imageSource, SKRect rect)
        {
            Rectangle = rect;
            ImageSource = imageSource;
        }

        //==================================================

        /*----------------------------
         * 
         * Where do we draw it? 
         * 
         * --------------------------*/

        internal SKRect rectangle_;
        public SKRect Rectangle
        {
            get => rectangle_;
            set => rectangle_ = value;
        }

        //==================================================

        /*---------------------------
         * 
         * What is the image? 
         * 
         * -------------------------*/

        internal string imageSource_;
        public string ImageSource
        {
            get => imageSource_;
            set
            {
                imageSource_ = value;
            }
        }

        //---------------------------------------

        internal SKBitmap image_;
        public SKBitmap Image
        {
            get => image_;
            set => image_ = value;
        }

    }
}
