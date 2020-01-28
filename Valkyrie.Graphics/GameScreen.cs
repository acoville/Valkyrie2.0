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
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace Valkyrie.Graphics
{
    public class GameScreen : Screen
    {
        internal SKColor ClearPaint = new SKColor(255, 255, 255, 0);

        internal SKBitmap background_;

        //==================================================================
        
        internal string backgroundSource_;
        public string BackgroundSource
        {
            get => backgroundSource_;
            set
            {
                backgroundSource_ = "Valkyrie.App.Images.Backgrounds." + value;

                Assembly assembly = GetType().GetTypeInfo().Assembly;

                try
                {
                    using (Stream stream = assembly.GetManifestResourceStream(backgroundSource_))
                    {
                        Background = SKBitmap.Decode(stream);
                    }
                }

                catch(Exception ex)
                {
                    string ErrMsg = "";

                    if(!string.IsNullOrEmpty(ErrMsg))
                    {
                        ErrMsg = ex.ToString() + Environment.NewLine;                        
                    }
                }

            }
        }

        //===================================================================

        public SKBitmap Background
        {
            get => background_;
            set
            {
                background_ = value;
            }
        }

        //========================================================

        //-- data relating to the mid-ground

        SKImage midground_;

        internal ObservableCollection<Sprite> sprites_;
        public ObservableCollection<Sprite> Sprites
        {
            get => sprites_;
            set
            {
                sprites_ = value;
            }
        }

        //-----------------------------------------

        internal ObservableCollection<Tile> tiles_;
        public ObservableCollection<Tile> Tiles
        {
            get => tiles_;
            set
            {
                tiles_ = value;
            }
        }

        //=====================================================

        // foreground

        SKImage foreground_;

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

            background_ = new SKBitmap(Info());
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

        /*---------------------------------------
         * 
         * Event Handler to redraw the screen
         * 
         * I need to find a way to cache the 
         * surface adn only redraw what is 
         * necessary, the performance on this
         * is bad
         * 
         * -------------------------------------*/

        public ICommand PaintCommand { get; set; }
        public void OnPainting(SKPaintGLSurfaceEventArgs args)
        {
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(ClearPaint);

            // draw background:

            //canvas.DrawBitmap(background_, new SKPoint(0, 0));

            // draw middle: 

            //      draw all sprites

            //      draw all static obstacles

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
