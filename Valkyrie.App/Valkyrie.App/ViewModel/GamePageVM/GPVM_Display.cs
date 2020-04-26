using System.ComponentModel;
using Valkryie.GL;
using Valkyrie.Graphics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        //============================================================

        /*--------------------------------------
         * 
         * We do quite a lot of work with the 
         * Screen and Sprite classes here
         * 
         * ------------------------------------*/

        internal GameScreen deviceScreen_;
        public GameScreen DeviceScreen
        {
            get => deviceScreen_;
        }

        //===============================================================

        //----------- control buttons opacity

        internal double controlOpacity_ = Preferences.Get("controlOpacity", 0.85);
        public double ControlOpacity
        {
            get => controlOpacity_;
            set => controlOpacity_ = value;
        }

        //==============================================================

        // Background Image streams from the GameScreen 

        internal ImageSource backgroundImage_;
        public ImageSource BackgroundImage
        {
            get => backgroundImage_;
            set => backgroundImage_ = value;
        }

        //==============================================================

        /*-----------------------------------
         * 
         * Troubleshooting information
         * Raw frames tracking info 
         * used to calculate FPS
         * 
         * ---------------------------------*/

        internal int frames_ = 0;
        public int Frames
        {
            get => frames_;

            set
            {
                if(value == 0)
                {
                    FPS = frames_;
                }

                frames_ = value;
                RaisePropertyChanged();
            }
        }

        //==============================================================

        /*------------------------------------
         * 
         * Troubleshooting Information 
         *  Framerate
         * 
         * ----------------------------------*/

        internal float fps_;
        public float FPS
        {
            get => fps_;
            set
            {
                fps_ = value;
                RaisePropertyChanged();
            }
        }

        //=============================================================

        /*-----------------------------------------
         * 
         * Control variables that dictate weather
         * debug info is displayed on-screen.
         * 
         * FPS and runtime environment share an 
         * element on GamePage.xaml, so both 
         * properties will check to see if that
         * element should be hidden or shown.
         * 
         * --------------------------------------*/

        internal bool displayFPS_ = Preferences.Get("display_FPS", false);

        public bool DisplayFPS
        {
            get => displayFPS_;
            set
            {
                displayFPS_ = value;
                RaisePropertyChanged(nameof(DisplayFPS));
            }
        }

        //=============================================================

        /*--------------------------------------
         * 
         * Runtime Environment Property
         * 
         * ------------------------------------*/

        internal string runtimeEnv_ = Device.RuntimePlatform.ToString();
        public string RuntimeEnv
        {
            get => runtimeEnv_;
        }

        /*--------------------------------------
         * 
         * Property controlling weather to 
         * display the Environment Runtime 
         * on-screen
         * 
         * ------------------------------------*/

        internal bool displayEnv_ = Preferences.Get("displayEnv", false);
        public bool DisplayEnv
        {
            get => displayEnv_;
            
            set
            {
                displayEnv_ = value;
                RaisePropertyChanged(nameof(DisplayEnv));
            }
        }

        //=====================================================================

        /*----------------------------------------
         * 
         *  Function called during resize events
         *  or scrolling to move all the drawables
         *  into place
         * 
         * -------------------------------------*/

        public void AlignGamePiecesToScreen()
        {
            foreach (var prop in props_)
            {
                SKPosition target = deviceScreen_.scrollBox_.ToSkia(prop.GLPosition);

                int height = prop.SKProp.DisplayImage.Height;
                target.Y -= height;

                prop.MoveSprite(target);
            }

            //--------------------------------------

            foreach (var obstacle in obstacles_)
            {
                GLPosition glOrigin = obstacle.GLPosition;
                SKPosition target = deviceScreen_.scrollBox_.ToSkia(glOrigin);

                obstacle.MoveSprite(target);
            }

            //--------------------------------------

            /*
             */ 

            foreach(var actor in actors_)
            {
                GLPosition glOrigin = actor.GLPosition;
                SKPosition target = deviceScreen_.scrollBox_.ToSkia(glOrigin);

                int height = actor.Sprite.DisplayImage.Height;
                target.Y -= height;

                actor.Sprite.Move(target);
            }
        }
    }
}