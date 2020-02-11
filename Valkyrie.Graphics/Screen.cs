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

        internal ScreenInfo info_;
        public ScreenInfo Info
        {
            get => info_;
        }

        //=================================================================

        public enum Orientation { portrait, landscape, square };
        
        public Orientation ScreenOrientation
        {
            get => info_.Orientation;
        }

        //================================================================

        public double Height
        {
            get => info_.Height;
        }

        //---------------------------------

        public double Width
        {
            get => info_.Width;
        }

        //================================================================

        public void GetScreenDetails()
        {
            var metrics = DeviceDisplay.MainDisplayInfo;
            info_ = new ScreenInfo(metrics.Height, metrics.Width);     
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
