﻿/*=============================================================================
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
using System.Threading.Tasks;
using Valkyrie.App.ViewModel;
using Xamarin.Essentials;
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

        //===================================================================

        /*--------------------------------------
         * 
         * Function to call SkiaSharp 
         * View's invalidate surface command
         * 
         * I would love to cache this somehow.. 
         * compare hashes perhaps? 
         * 
         * -----------------------------------*/

        public void Redraw()
        {
            SKGLView.InvalidateSurface();
        }

        //===================================================================

        /*---------------------------------------
         * 
         * Handler for screen rotations, 
         * 
         * -------------------------------------*/

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            gpvm_.DeviceScreen.GetScreenDetails();
            gpvm_.AlignGamePiecesToScreen();        // in GPVM_Display.cs
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Preferences.Get("Controller", "Virtual Gamepad") == "Virtual Gamepad")
            {
                gpvm_.DisplayVirtualController = true;
            }
            else
            {
                gpvm_.DisplayVirtualController = false;
            }

            gpvm_.ControlOpacity = Preferences.Get("controlOpacity", 0.85);
            gpvm_.DisplayEnv = Preferences.Get("displayEnv", false);
            gpvm_.DisplayFPS = Preferences.Get("display_FPS", false);
            
            DateTime t1 = DateTime.Now;
            DateTime t2;
            TimeSpan timeElapsed;

            Device.StartTimer(TimeSpan.FromMilliseconds(gpvm_.GameSpeed), () =>
            {
                if (gpvm_.Paused == false)
                {
                    #region collision detection

                    /*
                    
                    Task.Run(() =>
                    {
                    });
                     */

                        gpvm_.EvaluateMovement();

                    #endregion

                    //-- Redraw Screen, move to a dedicated render thread

                    #region render

                    RedrawScreen();

                    #endregion

                    #region framerate counter

                    //-- update the frame counter

                    gpvm_.Frames++;
                    t2 = DateTime.Now;
                    timeElapsed = t2 - t1;
                    
                    // the setter of the Frames property updates FPS

                    if(timeElapsed >= TimeSpan.FromSeconds(1.0))
                    {
                        gpvm_.Frames = 0;
                        t1 = DateTime.Now;
                    }

                    #endregion
                }

                return true;
            });
        }

        //===========================================================

        /*-------------------------------------------
         * 
         * Pause the game if player backs out to 
         * main menu
         * 
         * -----------------------------------------*/

        protected override void OnDisappearing()
        {
            gpvm_.Paused = true;
            base.OnDisappearing();
        }

        //===================================================================

        /*-------------------------------------
        * 
        * Event Handler for a click on the 
        * paused buttont
        * 
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