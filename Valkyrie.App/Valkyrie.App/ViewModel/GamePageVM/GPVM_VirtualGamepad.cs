

using System.ComponentModel;
using System.Windows.Input;
using Valkyrie.App.Model;
using Valkyrie.Controls;
using Xamarin.Forms;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        //====================================================================================

        /*------------------------------------------
         * 
         * Time to review some events I guess..
         * 
         * ----------------------------------------*/

        internal void Map_Controller_To_Actor(Actor actor, Controller controller)
        {
            actor.ControlStatus = controller.ControlStatus;
        }

        //==============================================================================

        internal Command leftCommand;
        public ICommand LeftCommand
        {
            get
            {
                return (ICommand)leftCommand ?? (leftCommand = new Command(() =>
                {
                    player1.TurnLeft();
                    player1.ControlStatus.DirectionalStatus.L = true;
                }));
            }
        }

        //==============================================================================

        internal Command leftReleaseCommand;
        public ICommand LeftReleaseCommand
        {
            get
            {
                return (ICommand)leftReleaseCommand ?? (leftReleaseCommand = new Command(() =>
                {
                    player1.ControlStatus.DirectionalStatus.L = false;
                }));
            }
        }

        //===============================================================================

        internal Command rightCommand;
        public ICommand RightCommand
        {
            get
            {
                return (ICommand)rightCommand ?? (rightCommand = new Command(() =>
                {
                    player1.TurnRight();
                    player1.ControlStatus.DirectionalStatus.R = true;
                }));
            }
        }

        //==============================================================================

        /*-----------------------------------------------
         * 
         * This appears not to be working correctly
         * sometimes, Bob just keeps sliding right.
         * But he's not accelerating, he's just 
         * coasting at whatever speed he was going..
         * 
         * ---------------------------------------------*/

        internal Command rightReleaseCommand;
        public ICommand RightReleaseCommand
        {
            get
            {
                return (ICommand)rightReleaseCommand ?? (rightReleaseCommand = new Command(()=>
                {
                     player1.ControlStatus.DirectionalStatus.R = false;
                }));
            }
        }

        //==============================================================================

        internal Command upCommand;
        public ICommand UpCommand
        {
            get
            {
                return (ICommand)upCommand ?? (upCommand = new Command(() =>
                 {
                     player1.ControlStatus.DirectionalStatus.U = true;
                 }));
            }
        }

        //==============================================================================

        internal Command upReleaseCommand;
        public ICommand UpReleaseCommand
        {
            get
            {
                return (ICommand)upReleaseCommand ?? (upReleaseCommand = new Command(() =>
                 {
                     player1.ControlStatus.DirectionalStatus.U = false;
                 }));
            }
        }

        //==============================================================================

        internal Command downCommand;
        public ICommand DownCommand
        {
            get
            {
                return (ICommand)downCommand ?? (downCommand = new Command(() =>
                 {
                     player1.ControlStatus.DirectionalStatus.D = true;
                 }));
            }
        }

        //================================================================================

        internal Command downReleaseCommand;
        public ICommand DownReleaseCommand
        {
            get
            {
                return (ICommand)downReleaseCommand ?? (downReleaseCommand = new Command(() =>
                 {
                     player1.ControlStatus.DirectionalStatus.D = false;
                 }));
            }
        }

        //==============================================================================

        internal Command upRightCommand;
        public ICommand UpRightCommand
        {
            get
            {
                return (ICommand)upRightCommand ?? (upRightCommand = new Command(() =>
                 {
                     player1.ControlStatus.DirectionalStatus.UR = true;
                 }));
            }
        }

        //=================================================================================

        internal Command upRightReleaseCommand;
        public ICommand UpRightReleaseCommand
        {
            get
            {
                return (ICommand)upRightReleaseCommand ?? (upRightReleaseCommand = new Command(() =>
                 {
                     player1.ControlStatus.DirectionalStatus.UR = false;
                 }));
            }
        }

        //==============================================================================

        internal Command downRightCommand;
        public ICommand DownRightCommand
        {
            get
            {
                return (ICommand)downRightCommand ?? (downRightCommand = new Command(() =>
                 {
                     player1.ControlStatus.DirectionalStatus.DR = true;
                 }));
            }
        }

        //==============================================================================

        internal Command downLeftCommand;
        public ICommand DownLeftCommand
        {
            get
            {
                return (ICommand)downLeftCommand ?? (downLeftCommand = new Command(() =>
                {
                    player1.ControlStatus.DirectionalStatus.DL = true;
                }));
            }
        }

        //==============================================================================

        internal Command downLeftReleaseCommand;
        public ICommand DownLeftReleaseCommand
        {
            get
            {
                return (ICommand)downLeftReleaseCommand ?? (downLeftReleaseCommand = new Command(() =>
                 {
                     player1.ControlStatus.DirectionalStatus.DL = false;
                 }));
            }
        }

        //==============================================================================

        internal Command upLeftCommand;
        public ICommand UpLeftCommand
        {
            get
            {
                return (ICommand)upLeftCommand ?? (upLeftCommand = new Command(() =>
                 {
                     player1.ControlStatus.DirectionalStatus.UL = true;
                 }));
            }
        }

        //==============================================================================

        internal Command upLeftReleaseCommand;
        internal ICommand UpLeftReleaseCommand
        {
            get
            {
                return (ICommand)upLeftReleaseCommand ?? (upLeftReleaseCommand = new Command(() =>
                {
                    player1.ControlStatus.DirectionalStatus.UL = false;
                }));
            }
        }

        //===============================================================================

        internal Command aCommand;
        public ICommand ACommand
        {
            get
            {
                return (ICommand)aCommand ?? (aCommand = new Command(() =>
                {
                    player1.Jump();
                    player1.ControlStatus.Jump = true;
                }));
            }
        }

        //==============================================================================

        internal Command aReleaseCommand;
        public ICommand AReleaseCommand
        {
            get
            {
                return (ICommand)aReleaseCommand ?? (aReleaseCommand = new Command(() =>
                 {
                     player1.ControlStatus.Jump = false;
                 }));
            }
        }

        //==============================================================================

        internal Command bCommand;
        public ICommand BCommand
        {
            get
            {
                return (ICommand)bCommand ?? (bCommand = new Command(() =>
                 {
                     player1.ControlStatus.Attack = true;
                 }));
            }
        }

        //==============================================================================

        internal Command bReleaseCommand;
        public ICommand BReleaseCommand
        {
            get
            {
                return (ICommand)bReleaseCommand ?? (bReleaseCommand = new Command(() =>
                 {
                     player1.ControlStatus.Attack = false;
                 }));
            }
        }
    }
}