/*================================================================
 * 
 * Valkyrie
 * Game Page View Model class
 * 
 * 
 * =============================================================*/

using Valkyrie.Graphics;
using Valkyrie.GL;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Valkryie.GL;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        public delegate void InputChangedHandler(Character c, string e);
        public event PropertyChangedEventHandler PropertyChanged;

        //===============================================================

        /*--------------------------------
         * 
         * Background Image
         * 
         * ------------------------------*/

        internal string backgroundImage_;
        public string BackgroundImage
        {
            get => backgroundImage_;
            set
            {
                backgroundImage_ = value;
                RaisePropertyChanged();
            }
        }

        //===============================================================

        //----------- control buttons opacity

        internal double controlOpacity_ = Preferences.Get("controlOpacity", 0.85);
        public double controlOpacity
        {
            get => controlOpacity_;
        }

        //============================================================

        /*--------------------------------------
         * 
         * We do quite a lot of work with the 
         * Screen and Sprite classes here
         * 
         * ------------------------------------*/

        internal Screen deviceScreen_;
        public Screen DeviceScreen
        {
            get => deviceScreen_;
        }

        //============================================================

        /*-----------------------------------
         * 
         * Navigation Bar becomes visible 
         * when pause button is pressed
         * 
         * ---------------------------------*/

        internal bool displayNavigationBar_ = false;
        public bool DisplayNavigationBar
        {
            get => displayNavigationBar_;
        }

        //===========================================================

        /*------------------------------------
         * 
         * Constructor
         * 
         * ----------------------------------*/
        public GamePageViewModel()
        {
            deviceScreen_ = new Screen();
            GameSpeed = 50;
        }

        //=============================================================

        /*----------------------------------
         * 
         * Pause logic
         * 
         * --------------------------------*/

        internal bool paused_ = false;
        public bool Paused
        {
            get => paused_;
            set
            {
                paused_ = value;
                displayNavigationBar_ = value;
                RaisePropertyChanged();
            }
        }

        //=================================================================

        /*-------------------------------------
         * 
         *  Current Level
         * 
         * Changing this results in loading a 
         * new map
         * 
         * -----------------------------------*/

        internal Level currentLevel_;
        public Level CurrentLevel
        {
            get => currentLevel_;
            set
            {
                currentLevel_ = value;
                LoadLevel(currentLevel_);
            }
        }

        //=================================================================

        /*------------------------------------------
         * 
         * Event Handler to raise propertyChanged
         * 
         * ---------------------------------------*/

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        //==================================================================

        //-- interface control variables

        /*----------------------------------------------

         * The Skia image is redrawn at a rate of
         *
         * 1,000 ms / 30 frames per second = 30.30
         * 1,000 ms / 20 frames per second = 50 
       
        --------------------------------------------*/

        internal double gameSpeed_;
        public double GameSpeed
        {
            get
            {
                return gameSpeed_;
            }
            set
            {
                //-- if within range

                if (!(value > 1000) && !(value < 15))
                {
                    gameSpeed_ = value;
                }

                //-- slowest speed: 1 frame / second

                else if (value > 1000)
                {
                    value = 1000;
                    gameSpeed_ = value;
                }

                //-- fastest speed: 66 frames / second

                else if (value < 15)
                {
                    value = 15;
                    gameSpeed_ = value;
                }

                RaisePropertyChanged();
            }
        }
    }
}
