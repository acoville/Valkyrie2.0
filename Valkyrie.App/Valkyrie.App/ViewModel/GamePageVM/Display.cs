

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
         * Troubleshooting Information
         * Runtime Environment
         * 
         * ------------------------------*/

        internal string env_;
        public string Env
        {
            get => env_;
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
    }
}