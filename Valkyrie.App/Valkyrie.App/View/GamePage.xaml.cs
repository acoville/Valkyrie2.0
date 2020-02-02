/*=============================================================================
 * 
 *  Valkyrie
 * 
 * GamePage represents an instance of the game engine running.
 * It will load a map into memory and all the actors, events, 
 * obstacles contained insdie it, then continue to redraw and
 * evaluate game logic until vicotry or other conditions 
 * cause the game to end or progress to the next level. 
 * 
 * ==========================================================================*/

using SkiaSharp.Views.Forms;
using System;
using Valkyrie.App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Valkyrie.App.View
{
    delegate void RedrawHandler();

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        event RedrawHandler RedrawScreen;

        internal GamePageViewModel gpvm_;

        //===================================================================

        /*-------------------------------------
         * 
         * Constructor
         * 
         * -----------------------------------*/

        public GamePage()
        {
            InitializeComponent();

            gpvm_ = new GamePageViewModel();
            BindingContext = gpvm_;
          
            RedrawScreen = new RedrawHandler(Redraw);
        }

        //====================================================================

        /*----------------------------------
         * 
         * Tracking info to see how much 
         * time has elapsed during gameplay
         * 
         * -------------------------------*/

        internal TimeSpan frameCounter_;


        //===================================================================

        /*--------------------------------------
         * 
         * Function to call SkiaSharp 
         * View's invalidate surface command
         * 
         * -----------------------------------*/

        public void Redraw()
        {
            SKGLView.InvalidateSurface();
            gpvm_.FPS++;
        }

        //===================================================================

        /*----------------------------------
         * 
         * Handler for screen rotations, 
         * resizes
         * 
         * -------------------------------*/

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            gpvm_.DeviceScreen.GetScreenDetails();
        }

        //==================================================================

        /*--------------------------------------
         * 
         * On Appearing
         * 
         * main loop responsible for evaluating
         * timed events in the UI thread
         * 
         * -----------------------------------*/

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.StartTimer(TimeSpan.FromMilliseconds(gpvm_.GameSpeed), () =>
            {
                if (gpvm_.Paused == false)
                {
                    //gpvm_.EvaluateMovement();

                    RedrawScreen();
                }

                return true;
            });
        }

        //===================================================================

        /*-------------------------------------
        * 
        * Event Handler for a click on the 
        * paused buttonti
        * -----------------------------------*/

        private void PauseButtonClicked(object sender, EventArgs e)
        {
            if (gpvm_.Paused)
            {
                gpvm_.Paused = false;
                Pause_Btn.Text = "PAUSE";
                return;
            }

            if (!gpvm_.Paused)
            {
                gpvm_.Paused = true;
                Pause_Btn.Text = "UNPAUSE";
                return;
            }
        }

        //========================================================================

        /*----------------------------------------------
         * 
         * I just cannot get the damn SKGLView to 
         * "behave" and do this through a beahvior
         * like I could a CanvasView. No idea
         * what I'm missing, but I'm sick of wasting
         * time on it. The handler will be wired through
         * here. Sue me. 
         * 
         * --------------------------------------------*/

        private void SKGLView_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            gpvm_.DeviceScreen.OnPaintSurface(e);
        }
    }
}