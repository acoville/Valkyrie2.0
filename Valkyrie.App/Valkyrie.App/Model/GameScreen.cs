﻿/*=========================================================
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
using Valkryie.GL;
using Valkyrie.App.Model;
using Xamarin.Essentials;
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

        internal bool troubleshooting_ = false;
        public bool Trouble
        {
            get => troubleshooting_;
            set => troubleshooting_ = value;
        }

        //==================================================================

        /*----------------------------
         * 
         * Scrollbox Troubleshooting info
         * 
         * ------------------------*/

        internal SKPaint scrollBoxPaint;
        internal SKPaint scrollTextPaint;

        internal SKColor ScrollboxColor = new SKColor(255, 0, 0, 75);
        internal SKColor ScrolltextColor = new SKColor(200, 200, 200, 255);

        internal bool displayScrollbox_ = Preferences.Get("displayScrollbox", false);

        //=================================================================

        internal ScrollBox scrollBox_;

        /*-----------------------------
         * 
         * Scrollbox
         * 

        public SKRect ScrollBox
        {
            get => scrollBox_.Skia;
            set => scrollBox_.Skia = value;
        }
         * ---------------------------*/

        //================================================================

        /*--------------------------
         * 
         * Highlight a block
         * 
         * ------------------------*/

        internal SKPaint BlockHighlightPaint;
        internal SKColor BlockHighlightColor = new SKColor(0, 255, 0, 75);

        //========================================================

        /*---------------------------------
         * 
         * All the dynamic 2D character
         * sprites on screen
         * 
         * -----------------------------*/

        internal ObservableCollection<Drawable> sprites_;
        public ObservableCollection<Drawable> Sprites
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

        //============================================================

        /*----------------------------------
         *
         *  Function to Add a Tile
         * 
         * --------------------------------*/

        public void AddTileGroup(Obstacle val)
        {
            GLPosition origin = val.obstacle_.Rectangle.Origin;

            SKPoint target = scrollBox_.ToSkia(origin);
            
            val.Tiles.Move(target);

            tiles_.Add(val.Tiles);
        }

        //===========================================================

        /*-----------------------------------------
         * 
         * Prop objects are any image that 
         * contributes to the mood, decor and
         * environmental feel of the composed
         * image but is non-interactive to 
         * the player. For the moment, I do not
         * need any more complex logic than what
         * is in the base Drawable class to do 
         * this.
         * 
         * ---------------------------------------*/

        internal ObservableCollection<Drawable> props_;
        public void AddProp(Prop arg)
        {
            SKPoint target = scrollBox_.ToSkia(arg.GLProp.GLPosition);

            arg.SKProp.Move(target);

            int height = arg.SKProp.DisplayImage.Height * -1;

            arg.SKProp.Translate(0, height);

            props_.Add(arg.SKProp);
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

            scrollBox_ = new ScrollBox(Info);

            sprites_ = new ObservableCollection<Drawable>();
            tiles_ = new ObservableCollection<TileGroup>();
            props_ = new ObservableCollection<Drawable>();

            PrepareTroubleshootingInfo();
        }

        //==============================================================

        /*---------------------------------
         * 
         * Helper Function to initialize
         * troubleshooting data members
         * 
         * -------------------------------*/

        internal void PrepareTroubleshootingInfo()
        {
            // initialize variables used in troubleshooting

            scrollBoxPaint = new SKPaint
            {
                Color = ScrollboxColor,
                Style = SKPaintStyle.StrokeAndFill,
                IsAntialias = true
            };

            scrollTextPaint = new SKPaint
            {
                Color = ScrolltextColor,
                Style = SKPaintStyle.StrokeAndFill,
                TextSize = 32.0f,
                IsAntialias = true
            };

            // initialize block highlighter info

            BlockHighlightPaint = new SKPaint
            {
                Color = BlockHighlightColor,
                Style = SKPaintStyle.StrokeAndFill,
                IsAntialias = true
            };
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

            // draw all props

            foreach(var prop in props_)
            {
                canvas.DrawBitmap(prop.DisplayImage, prop.SkiaOrigin);
            }

            //  draw all sprites



            //  draw all static obstacles
                            
            foreach(var tileGroup in tiles_)
            {
                foreach(var row in tileGroup.Tiles)
                {
                    foreach(var col in row)
                    {
                        canvas.DrawBitmap(col.DisplayImage, col.SkiaOrigin);
                    }
                }
            }

            // troubleshooting artifacts enabled in developer mode

            if(Preferences.Get("displayScrollbox", false))
            {
                DrawScrollBox(canvas);
            }
        }

        //=====================================================================

        /*----------------------------------
         * 
         * Helper Functions to render
         * troubleshooting information 
         * on-screen.
         * 
         * --------------------------------*/

        internal void DrawScrollBox(SKCanvas canvas)
        {
            canvas.DrawRect(scrollBox_.Skia, scrollBoxPaint);

            SKRect rect = scrollBox_.Skia;

            SKPoint textPoint = new SKPoint(rect.Left + 10,
                                            rect.Top + 50);

            canvas.DrawText("Camera ScrollBox", textPoint, scrollTextPaint);
        }
    }
}
