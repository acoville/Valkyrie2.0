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

        internal SKColor ClearPaint = new SKColor(255, 255, 255, 0);
        internal SKColor ScrollboxColor = new SKColor(255, 0, 0, 75);
        internal SKColor ScrolltextColor = new SKColor(200, 200, 200, 255);
        internal SKColor BlockHighlightColor = new SKColor(0, 255, 0, 75);

        internal SKPaint scrollBoxPaint;
        internal SKPaint scrollTextPaint;
        internal SKPaint BlockHighlightPaint;

        internal ObservableCollection<Drawable> sprites_;
        internal ObservableCollection<Obstacle> obstacles_;
        internal ObservableCollection<Prop> props_;

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
            GLPosition glOrigin = val.Position.GLPosition;
            SKPoint target = scrollBox_.ToSkia(glOrigin);

            val.Tiles.Move(target);
            obstacles_.Add(val);
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

            if(initialized_)
            {
                scrollBox_.Update(Info);
                AlignPiecesToScrollBox();
            }
        }

        //===========================================================

        internal void AlignPiecesToScrollBox()
        {
            foreach(var obstacle in obstacles_)
            {
                GLPosition glPos = obstacle.Position.GLPosition;
                SKPoint target = scrollBox_.ToSkia(glPos);
                
                //obstacle.Position.skiaPosition_ = target;
                //obstacle.Tiles.Move(target);


            }

            //--------------------------------------

            foreach (var prop in props_)
            {
                SKPoint target = scrollBox_.ToSkia(prop.Position.GLPosition);

                int height = prop.SKProp.DisplayImage.Height;
                target.Y -= height;

                prop.SKProp.Move(target);
            }

            //--------------------------------------
            /*
            foreach(var actor in sprites_)
            {

            }
             */
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

        public void AddProp(Prop arg)
        {
            SKPoint target = scrollBox_.ToSkia(arg.Position.GLPosition);

            int height = arg.SKProp.DisplayImage.Height;
            target.Y -= height;

            arg.SKProp.Move(target);
            props_.Add(arg);
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
            obstacles_ = new ObservableCollection<Obstacle>();
            props_ = new ObservableCollection<Prop>();

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
            // ----- ! are these the right type? ! ----

            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(ClearPaint);

            // draw all props

            foreach(var prop in props_)
            {
                canvas.DrawBitmap(prop.skiaProp_.DisplayImage, prop.skiaProp_.SkiaOrigin);
            }

            //  draw all sprites



            //  draw all static obstacles
            
            foreach(var obstacle in obstacles_)
            {
                foreach(var row in obstacle.Tiles.Tiles)
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
