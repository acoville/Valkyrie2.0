using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Valkyrie.Graphics
{
    public class Screen
    {
        public enum Orientation { portrait, landscape, square };
        internal Orientation orientation_;
        public Orientation ScreenOrientation
        {
            get
            {
                return orientation_;
            }
        }

        //================================================================

        internal double height_;
        public double Height
        {
            get
            {
                return height_;
            }
        }

        //---------------------------------

        internal double width_;
        public double Width
        {
            get
            {
                return width_;
            }
        }

        //================================================================

        public SKSurface Surface { get; set; }
        internal SKImageInfo ScreenDetails()
        {
            SKImageInfo info = new SKImageInfo((int)Width, (int)Height);
            return info;
        }

        //================================================================

        public void GetScreenDetails()
        {
            var metrics = DeviceDisplay.MainDisplayInfo;

            // determine orientation

            width_ = metrics.Width;
            height_ = metrics.Height;
            
            // shrink the render area to accomodate the 
            // virtual controls, D-Pad and actionbuttons
            
            height_ *= .75;

            // screen is currently in portrait

            if(Height > Width)
            {
                orientation_ = Orientation.portrait;
            }

            // screen is currently in landscape

            else if (Height < Width)
            {
                orientation_ = Orientation.landscape;
            }

            // must be a square

            else
            {
                orientation_ = Orientation.square;
            }


        }

        //================================================================

        public Screen()
        {
            GetScreenDetails();
        }
    }
}
