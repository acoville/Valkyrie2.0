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
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Valkyrie.Graphics
{
    public class GameScreen : Screen
    {
        internal SKColor ClearPaint = new SKColor(255, 255, 255, 0);

        //========================================================

        internal ObservableCollection<Sprite> sprites_;
        public ObservableCollection<Sprite> Sprites
        {
            get => sprites_;
            set => sprites_ = value;
        }

        //-----------------------------------------

        internal ObservableCollection<Tile> tiles_;
        public ObservableCollection<Tile> Tiles
        {
            get => tiles_;
            set => tiles_ = value;
        }

        //=====================================================

        public SKSurface Surface { get; set; }

        //============================================================

        public GameScreen()
        {
            // base class constructor called first, so it already has 
            // the native display screen details

            Redraw = new Command<SKPaintGLSurfaceEventArgs>(OnPainting);
            PaintCommand = Redraw;

            Sprites = new ObservableCollection<Sprite>();
            Tiles = new ObservableCollection<Tile>();
        }

        //===============================================================================

        /*-------------------------------------
         * 
         * Helper function to return an 
         * SKImageInfo object
         * 
         * -----------------------------------*/

        public SKImageInfo Info()
        {
            SKImageInfo info = new SKImageInfo((int)width_, (int)height_);
            return info;
        }

        //==============================================================

        /*------------------------------------------
         * 
         * Event Handler to redraw the screen
         * 
         * I need to find a way to cache the 
         * surface adn only redraw what is 
         * necessary, the performance on this
         * is bad
         * 
         * ---------------------------------------*/

        public ICommand PaintCommand { get; set; }
        public void OnPainting(SKPaintGLSurfaceEventArgs args)
        {
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(ClearPaint);

            //  draw all sprites

            //  draw all static obstacles

            foreach(var tile in Tiles)
            {
                //canvas.DrawRect(tile.Rectangle, )

                canvas.DrawBitmap(tile.Image, tile.Rectangle.Location);
            }

            // draw foreground:
        }

        //===============================================================

        /*--------------------------------
         * 
         * OnCanvasViewPaintSurface
         * 
         * -----------------------------*/

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
        }
    }
}
