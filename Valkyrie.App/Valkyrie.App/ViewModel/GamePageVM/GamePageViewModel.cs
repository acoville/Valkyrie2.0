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
using Valkryie.GL;
using Xamarin.Forms;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        public delegate void InputChangedHandler(Character c, string e);
        public event PropertyChangedEventHandler PropertyChanged;

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
            deviceScreen_ = new GameScreen();
            GameSpeed = 15;
            env_ = Device.RuntimePlatform.ToString();
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
                RaisePropertyChanged();
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        //==================================================================

        //-- interface control variables

        /*----------------------------------------------

         * The Skia image is redrawn at a rate of
         *
         * 1,000 ms / 30 frames per second = 30.30
         * 1,000 ms / 20 frames per second = 50 
         * 1,000 ms / 15 fps = 66
       
        --------------------------------------------*/

        internal double gameSpeed_ = 15;
        public double GameSpeed
        {
            get
            {
                return gameSpeed_;
            }
            set
            {
                gameSpeed_ = 1000 / value;
                RaisePropertyChanged();
            }
        }
    }
}
