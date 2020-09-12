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

        internal bool initialized_ = false;
        internal int sizeAllocations_ = 0;
        
        internal SKColor ClearPaint = new SKColor(255, 255, 255, 0);
        internal SKColor ScrollboxColor = new SKColor(255, 0, 0, 75);
        internal SKColor ScrolltextColor = new SKColor(200, 200, 200, 255);
        internal SKColor BlockHighlightColor = new SKColor(0, 255, 0, 75);

        internal SKPaint scrollBoxPaint;
        internal SKPaint scrollTextPaint;
        internal SKPaint BlockHighlightPaint;

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
            set => scrollBox_ = value;
        }

        //============================================================

        /*----------------------------------
         *
         *  Function to Add a Tile
         * 
         * --------------------------------*/

        public void AddObstacle(Obstacle val)
        {
            // find out where this should be displayed using the Scrollbox

            GLPosition glOrigin = val.GLPosition;
            SKPosition target = scrollBox_.ToSkia(glOrigin);

            val.MoveSprite(target);
            
            // add it to the list GameScreen.drawables_ 

            for(int i = 0; i < val.TilesGroup.Tiles.Count; i++)
            {
                var row = val.TilesGroup.Tiles[i];

                for(int j = 0; j < row.Count; j++)
                {
                    var tile = row[j];
                    drawables_.Add(tile);
                }
            }

            // sorting ensures that it is rendered at the correct Z depth

            drawables_.Sort();
        }

        //===========================================================

        /*----------------------------------------
         * 
         * Helper Function to Add an Actor
         * 
         * -------------------------------------*/

        public void AddActor(Actor actor)
        {
            SKPosition target = scrollBox_.ToSkia(actor.GLPosition);

            // determine starting image to use

            actor.Sprite.Status = Status.standing;

            //-- correct Y

            int height = actor.Sprite.DisplayImage.Height;
            target.Y -= height;

            //-- correct X 

            int width = actor.Sprite.DisplayImage.Width;
            target.X += (width / 2.0f);

            actor.Sprite.Move(target);
            drawables_.Add(actor.Sprite);

            drawables_.Sort();
        }

        //===========================================================

        /*-----------------------------------------
         * 
         * Function to Add a Prop
         * 
         * ---------------------------------------*/

        public void AddProp(Prop arg)
        {
            SKPosition target = scrollBox_.ToSkia(arg.GLPosition); 
            
            //-- correct Y 

            int height = arg.SKProp.DisplayImage.Height;
            target.Y -= height;

            //-- correct X 

            //int width = arg.SKProp.DisplayImage.Width;
            //target.X += (width / 2.0f);

            //-- move into position

            arg.MoveSprite(target);
            drawables_.Add(arg.SKProp);

            // the call to sort here is necessary to ensure that 
            // arg is now drawn in the correct Z layer

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
            }

            sizeAllocations_++;
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

            // draw everything in the drawables_ list, back to front

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

            if(Preferences.Get("displayCaptions", false))
            {
                string skiaCoords = drawable.SKPosition.ToString();
                SKPoint target = drawable.SKPosition.SKPoint;
                canvas.DrawText(skiaCoords, target, scrollTextPaint);
            }
        }
    }
}
