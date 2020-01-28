/*====================================================
 * 
 * Valkyrie.Graphics
 * Screen base class
 * 
 * detects native display information, 
 * determines screen orientation. This 
 * class is used by the menu pages to 
 * select the most appropriate background
 * image during OnSizeAllocated() events.
 * 
 * ================================================*/

using Xamarin.Essentials;
using Xamarin.Forms;

namespace Valkyrie.Graphics
{

    public partial class Screen
    {
        public Command Redraw { get; set; }

        //================================================================

        public enum Orientation { portrait, landscape, square };
        internal Orientation orientation_;
        public Orientation ScreenOrientation
        {
            get => orientation_;
        }

        //================================================================

        internal double height_;
        public double Height
        {
            get => height_;
        }

        //---------------------------------

        internal double width_;
        public double Width
        {
            get => width_;
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

        /*--------------------------------
         * 
         * Automatically detect
         * display properties when 
         * constructed
         *
         * -----------------------------*/

        public Screen()
        {
            GetScreenDetails();
        }
    }
}
