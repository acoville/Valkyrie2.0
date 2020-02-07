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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Valkyrie.Graphics
{
    public class GameScreen : Screen
    {
        public Command Redraw { get; set; }

        internal SKColor ClearPaint = new SKColor(255, 255, 255, 0);

        //==================================================================

        /*----------------------------
         * 
         * Troubleshooting info
         * 
         * ------------------------*/

        internal bool troubleshooting_ = true;
        public bool Trouble
        {
            get => troubleshooting_;
            set => troubleshooting_ = value;
        }

        //==================================================================

        /*----------------------------
         * 
         * Scrollbox info
         * 
         * ------------------------*/
        
        internal SKColor ScrollboxPaint = new SKColor(255, 0, 0, 100);

        internal SKRect scrollBox_;
        public SKRect ScrollBox
        {
            get => scrollBox_;
            set => scrollBox_ = value;
        }

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

        internal ObservableCollection<TileGroup> tiles_;
        public ObservableCollection<TileGroup> Tiles
        {
            get => tiles_;
            set => tiles_ = value;
        }

        //============================================================

        internal SKImageInfo ScreenDetails()
        {
            double width_ = info_.Width;
            double height_ = info_.Height;

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
            scrollBox_ = new SKRect();

            Sprites = new ObservableCollection<Sprite>();
            Tiles = new ObservableCollection<TileGroup>();
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
                            
            foreach(var tileGroup in Tiles)
            {
                foreach(var row in tileGroup.Tiles)
                {
                    foreach(var col in row)
                    {
                        canvas.DrawBitmap(col.Image, col.Rectangle.Location);
                    }
                }
            }

            // if GPVM.Trouble is enabled, display additional
            // sprites on-screen to aide in troubleshooting

            if(troubleshooting_)
            {
                var paint1 = new SKPaint
                {
                    TextSize = 64.0f,
                    IsAntialias = true,
                    Color = ScrollboxPaint,
                    Style = SKPaintStyle.StrokeAndFill
                };

                canvas.DrawRect(ScrollBox, paint1);
            }
        }
    }
}
