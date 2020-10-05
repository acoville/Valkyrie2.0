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
using System.Threading.Tasks;
using System.Linq;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        public delegate void InputChangedHandler(GLCharacter c, string e);
        public event PropertyChangedEventHandler PropertyChanged;

        internal Collision_Resolver collider;

        internal Controller VirtualGamepad = new Controller();
        
        internal Actor player1;

        //===========================================================

        /*------------------------------------
         * 
         * Constructor
         * 
         * ----------------------------------*/
        public GamePageViewModel()
        {
            deviceScreen_ = new GameScreen();
            
            //-- need to initialize the Controllers list and add 
            // the virtual gamepad to controllers_[0] 

            controllers_.Add(VirtualGamepad);

            collider = new Collision_Resolver();
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
            player1 = actors_.ElementAt(0);

            //-- determine control type

            string input = Preferences.Get("Controller", "Virtual Gamepad");

            switch (input)
            {
                case ("Virtual Gamepad"):
                {
                    DisplayVirtualController = true;
                    Map_Controller_To_Actor(player1, VirtualGamepad);
                    break;
                }

                //---------------------------------------
                /*
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
                 */
            }
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
       
            I did see some improvement in the Android VM 
            when I decreased this gamespeed from 30 to 15.

            UWP renders at 56 fps on localhost

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

        //===================================================================

        /*-------------------------------------
         * 
         * Evaluate Motion
         * 
         * -----------------------------------*/

        public void EvaluateMovement()
        {
            Parallel.ForEach(actors_, (actor) =>
            {
                if(!actor.Stationary)
                {
                    collider.EvaluateMotion(actor);
                    actor.Accelerate();
                }
            });

            /*

            foreach (var actor in actors_)
            {
                if (!actor.Stationary)
                {
                    collider.EvaluateMotion(actor);
                    actor.Accelerate();
                }
            }
             */
        }
    }
}
