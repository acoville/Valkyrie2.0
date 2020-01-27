using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Windows.Input;
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

        //===============================================================================

        /*-------------------------------------
         * 
         * Helper function to return an 
         * SKImageInfo object
         * 
         * 
         * -----------------------------------*/

        public SKImageInfo Info()
        {
            SKImageInfo info = new SKImageInfo((int)width_, (int)height_);
            return info;
        }

        //===============================================================

        /*-------------------------------------------------------
         * 
         * ClearPaint has an alpha channel of 0, making the empty 
         * pixels in teh image transparent so any images beneath
         * may be seen
         * 
         * --------------------------------------------------------*/

        internal SKColor ClearPaint = new SKColor(255, 255, 255, 0);

        //==============================================================

        /*--------------------------------------
         * 
         * Event Handler to redraw the screen
         * 
         * I need to find a way to cache the 
         * surface adn only redraw what is 
         * necessary, the performance on this
         * is bad
         * 
         * -----------------------------------*/

        public ICommand PaintCommand { get; set; }
        public void OnPainting(SKPaintSurfaceEventArgs args)
        {
            //SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(ClearPaint);

            // draw all sprites

            // draw all static obstacles
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

        //===============================================================

        /*--------------------------------
         * 
         * OnCanvasViewPaintSurface
         * 
         * -----------------------------*/

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            //SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
        }

        //================================================================

        public Screen()
        {
            GetScreenDetails();

            Redraw = new Command<SKPaintSurfaceEventArgs>(OnPainting);
            PaintCommand = Redraw;
        }

        //================================================================

        // this property required to instantiate an SKSurfaceEventArgs object

        public SKSurface Surface { get; set; }
        internal SKImageInfo ScreenDetails()
        {
            SKImageInfo info = new SKImageInfo((int)Width, (int)Height);
            return info;
        }
    }
}
