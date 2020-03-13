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
using System.Collections.Generic;
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

        internal bool displayScrollbox_ = Preferences.Get("displayScrollbox", false);
        internal bool initialized_ = false;

        internal int sizeAllocations_ = 0;

        internal SKColor ClearPaint = new SKColor(255, 255, 255, 0);
        internal SKColor ScrollboxColor = new SKColor(255, 0, 0, 75);
        internal SKColor ScrolltextColor = new SKColor(200, 200, 200, 255);
        internal SKColor BlockHighlightColor = new SKColor(0, 255, 0, 75);

        internal SKPaint scrollBoxPaint;
        internal SKPaint scrollTextPaint;
        internal SKPaint BlockHighlightPaint;

        internal List<Drawable> sprites_;
        internal List<Obstacle> obstacles_;
        internal List<Prop> props_;

        internal List<IDrawable> drawables_;

        //==================================================================

        /*----------------------------------
         * 
         * Scrollbox and related control,
         * utility variables
         * 
         * --------------------------------*/

        internal ScrollBox scrollBox_;
        public ScrollBox ScrollBox
        {
            get => scrollBox_;
            
            set
            {
                /*----------------------------------------------
                 * 
                 * We are only updating the Skia coordinates,
                 * not the GL coordinates
                 * 
                 * ------------------------------------------*/

                SKPoint oldOrigin = scrollBox_.Skia.Location;

                scrollBox_ = value;

                SKPoint newOrigin = scrollBox_.Skia.Location;

                if(oldOrigin != newOrigin)
                {
                    float deltaX = oldOrigin.X - newOrigin.X;
                    float deltaY = oldOrigin.Y - newOrigin.Y;
                }
            }
        }

        //============================================================

        /*----------------------------------
         *
         *  Function to Add a Tile
         * 
         * --------------------------------*/

        public void AddObstacle(Obstacle val)
        {
            GLPosition glOrigin = val.GLPosition;

            SKPosition target = scrollBox_.ToSkia(glOrigin);

            val.MoveSprite(target);
            obstacles_.Add(val);

            for(int i = 0; i < val.TilesGroup.Tiles.Count; i++)
            {
                var row = val.TilesGroup.Tiles[i];

                for(int j = 0; j < row.Count; j++)
                {
                    var tile = row[j];
                    drawables_.Add(tile);
                }
            }

            drawables_.Sort();
        }

        //===========================================================

        /*-----------------------------------------
         * 
         * Function to Add an Obstacle
         * 
         * ---------------------------------------*/

        public void AddProp(Prop arg)
        {
            SKPosition target = scrollBox_.ToSkia(arg.GLPosition);

            int height = arg.SKProp.DisplayImage.Height;
            target.Y -= height;

            arg.MoveSprite(target);

            props_.Add(arg);

            drawables_.Add(arg.SKProp);
            drawables_.Sort();
        }

        //===========================================================

        /*------------------------------------------
         * 
         * Override of Screen.GetScreenDetails()
         * will detect native display resolution, 
         * 
         * then adjust the scrollbox and realign
         * game pieces to where they need to be.
         * 
         * --------------------------------------*/

        public override void GetScreenDetails()
        {
            base.GetScreenDetails();

            if(sizeAllocations_ > 1)
            {
                scrollBox_.Update(Info);
                AlignPiecesToScrollBox();
            }

            sizeAllocations_++;
        }

        //===========================================================

        internal void AlignPiecesToScrollBox()
        {

            //--------------------------------------

            foreach (var prop in props_)
            {
                SKPosition target = scrollBox_.ToSkia(prop.GLPosition);

                int height = prop.SKProp.DisplayImage.Height;
                target.Y -= height;

                prop.MoveSprite(target);
            }

            //--------------------------------------

            foreach(var obstacle in obstacles_)
            {
                GLPosition glOrigin = obstacle.GLPosition;
                SKPosition target = scrollBox_.ToSkia(glOrigin);
                
                obstacle.MoveSprite(target);
            }

            
            /*
            foreach(var actor in sprites_)
            {

            }
             */
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

            sprites_ = new List<Drawable>();
            obstacles_ = new List<Obstacle>();
            props_ = new List<Prop>();
            drawables_ = new List<IDrawable>();

            PrepareTroubleshootingInfo();
            initialized_ = true;
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
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(ClearPaint);

            /*
             */

            foreach(var drawable in drawables_)
            {
                DrawDrawable(drawable, args);
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
         * the scrollbox on screen
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

        //=====================================================================

        /*----------------------------------------
         * 
         * Helper Function to rener one of the 
         * objects in the List<Drawable> drawables_
         * 
         * --------------------------------------*/

        internal void DrawDrawable(IDrawable drawable, SKPaintGLSurfaceEventArgs args)
        {
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.DrawBitmap(drawable.DisplayImage, drawable.SKPosition.SKPoint);

            if(Preferences.Get("Labels", false))
            {
                string skiaCoords = drawable.SKPosition.ToString();
                SKPoint target = drawable.SKPosition.SKPoint;
                canvas.DrawText(skiaCoords, target, scrollTextPaint);
            }
        }
    }
}
