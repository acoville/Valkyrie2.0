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
using Valkyrie.App.Model;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        public delegate void InputChangedHandler(GLCharacter c, string e);
        public event PropertyChangedEventHandler PropertyChanged;

        //===========================================================

        /*------------------------------------
         * 
         * Constructor
         * 
         * ----------------------------------*/
        public GamePageViewModel()
        {
            deviceScreen_ = new GameScreen();
            actors_ = new List<Actor>();
            obstacles_ = new List<Obstacle>();
            props_ = new List<Prop>();

            SetupPlayerOne();
        }

        //=============================================================

        /*------------------------------------------
         * 
         * Helper function to setup control
         * of player1
         * 
         * --------------------------------------*/

        internal void SetupPlayerOne()
        {
            //-- set up player control

            string input = Preferences.Get("Controller", "Virtual Gamepad");

            switch (input)
            {
                case ("Virtual Gamepad"):
                {
                    DisplayVirtualController = true;
                    break;
                }

                //---------------------------------------

                case ("Keyboard + Mouse"):
                {
                    DisplayVirtualController = false;
                    break;
                }

                //---------------------------------------

                case ("Gamepad"):
                {
                    DisplayVirtualController = false;
                    break;
                }
            }
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

        /*----------------------------------------------

         * The Skia image is redrawn at a rate of
       
        --------------------------------------------*/

        internal double gameSpeed_ = 30;
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
