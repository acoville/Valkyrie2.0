

using System.ComponentModel;
using Valkryie.GL;
using Valkyrie.App.Model;
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

        /*----------------------------------
         * 
         * Control variable displays debug
         * information on screen
         * 
         * -------------------------------*/

        internal bool troubleVisibile_ = true;
        public bool Trouble_Visible
        {
            get => troubleVisibile_;

            set
            {
                troubleVisibile_ = value;
                DeviceScreen.Trouble = value;
                RaisePropertyChanged();
            }
        }

        //=============================================================

        internal bool displayScrollbox_ = Preferences.Get("displayScrollbox", false);
        public bool DisplayScrollbox
        {
            get => displayScrollbox_;

            set
            {
                Preferences.Set("displayScrollbox", value);
                deviceScreen_.displayScrollbox_ = value;
                RaisePropertyChanged(nameof(DisplayScrollbox));
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
                if(value == true)
                {
                    if(!troubleVisibile_)
                    {
                        displayFPS_ = value;
                        Trouble_Visible = value;
                    }                    
                }

                else
                {
                    if(!displayEnv_)
                    {
                        displayFPS_ = value;
                        Trouble_Visible = false;
                    }
                }

                RaisePropertyChanged();
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
                if(value == true)
                {
                    if (!troubleVisibile_)
                    {
                        displayEnv_ = value;
                        Trouble_Visible = value;
                    }
                }

                else
                {
                    if (!displayFPS_)
                    {
                        displayEnv_ = value;
                        Trouble_Visible = false;
                    }
                }
    
                RaisePropertyChanged();
            }
        }
    }
}