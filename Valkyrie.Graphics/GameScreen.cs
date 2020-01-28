/*=========================================================
 * 
 * Valkyrie.Graphics
 * Game Screen derived class
 * 
 * 
 * 
 * =====================================================*/

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Windows.Input;
using Xamarin.Forms;

namespace Valkyrie.Graphics
{
    public class GameScreen : Screen
    {
        public GameScreen()
        {
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
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(ClearPaint);

            // draw all sprites

            // draw all static obstacles
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
    }
}
