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
        public Command Redraw { get; set; }

        internal SKColor ClearPaint = new SKColor(255, 255, 255, 0);

        //========================================================

        /*---------------------------------
         * 
         * All the dynamic 2D character
         * sprites on screen
         * 
         * -----------------------------*/

        internal ObservableCollection<Sprite> sprites_;
        public ObservableCollection<Sprite> Sprites
        {
            get => sprites_;
            set => sprites_ = value;
        }

        //=======================================================

        /*------------------------------------
         * 
         * All the static 2D tiles on-screen
         * 
         * ----------------------------------*/

        internal ObservableCollection<Tile> tiles_;
        public ObservableCollection<Tile> Tiles
        {
            get => tiles_;
            set => tiles_ = value;
        }

        //============================================================

        internal SKImageInfo ScreenDetails()
        {
            SKImageInfo info = new SKImageInfo((int)width_, (int)height_);
            return info;
        }

        //============================================================

        /*-------------------------------------
         * 
         * Constructor
         * 
         * ----------------------------------*/

        public GameScreen()
        {
            // base class constructor called first, so it already has 
            // the native display screen details
            
            Redraw = new Command<SKPaintGLSurfaceEventArgs>(OnPaintSurface);
            PaintCommand = Redraw;

            Sprites = new ObservableCollection<Sprite>();
            Tiles = new ObservableCollection<Tile>();
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
        public void OnPaintSurface(SKPaintGLSurfaceEventArgs args)
        {
            // ----- ! are these the right type? ! ----

            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(ClearPaint);

            //  draw all sprites

            //  draw all static obstacles
                            
            foreach(var tile in Tiles)
            {
                canvas.DrawBitmap(tile.Image, tile.Rectangle.Location);
            }
        }
    }
}
