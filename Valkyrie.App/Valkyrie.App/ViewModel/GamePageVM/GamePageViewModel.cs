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
using Valkyrie.Controls;
using System.Collections.ObjectModel;

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

            controllers_ = new List<IController>();

            Controller virtualGamepad = new Controller();
            controllers_.Add(virtualGamepad);

            SetupPlayerOneController();
        }

        //=============================================================

        /*------------------------------------------
         * 
         * Helper function to setup control
         * of player1
         * 
         * --------------------------------------*/

        internal void SetupPlayerOneController()
        {
            Controller p1Controller = new Controller();

            //-- determine control type

            string input = Preferences.Get("Controller", "Virtual Gamepad");

            switch (input)
            {
                case ("Virtual Gamepad"):
                {
                    DisplayVirtualController = true;
                    MapVirtualGamepad(p1Controller);
                    break;
                }

                //---------------------------------------

                case ("Keyboard + Mouse"):
                {
                    DisplayVirtualController = false;
                    MapKBM(p1Controller);
                    break;
                }

                //---------------------------------------

                case ("Gamepad"):
                {
                    DisplayVirtualController = false;
                    MapGamepad(p1Controller);
                    break;
                }
            }

            // whatever is now controlling that controller, 
            // add the player1 controller to gpvm.controllers_[0]

            controllers_.Add(p1Controller);
        }

        //=============================================================

        internal void MapKBM(Controller xcontroller)
        {

        }

        //==============================================================

        internal void MapGamepad(Controller xcontroller)
        {
        
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
