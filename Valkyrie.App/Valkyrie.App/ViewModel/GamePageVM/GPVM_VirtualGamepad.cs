

using System.ComponentModel;
using System.Windows.Input;
using Valkyrie.Controls;
using Windows.ApplicationModel.Store.Preview.InstallControl;
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

        internal void MapVirtualGamepad(Controller xcontroller)
        {

        }

        //==============================================================================

        internal Command leftCommand;
        public ICommand LeftCommand
        {
            get
            {
                return (ICommand)leftCommand ?? (leftCommand = new Command(() =>
                {
                    //controllers_[0].ControlStatus.DirectionalStatus.L = true;

                    actors_[0].TurnLeft();
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
                    controllers_[0].ControlStatus.DirectionalStatus.L = false;
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
                     //controllers_[0].ControlStatus.DirectionalStatus.R = true;

                     actors_[0].TurnRight();
                 }));
            }
        }

        //==============================================================================

        internal Command rightReleaseCommand;
        public ICommand RightReleaseCommand
        {
            get
            {
                return (ICommand)rightReleaseCommand ?? (rightReleaseCommand = new Command(() =>
                 {
                     controllers_[0].ControlStatus.DirectionalStatus.R = false;
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
                     controllers_[0].ControlStatus.DirectionalStatus.U = true;
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
                     controllers_[0].ControlStatus.DirectionalStatus.U = false;
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
                     controllers_[0].ControlStatus.DirectionalStatus.D = true;
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
                     controllers_[0].ControlStatus.DirectionalStatus.D = false;
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
                     controllers_[0].ControlStatus.DirectionalStatus.UR = true;
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
                     controllers_[0].ControlStatus.DirectionalStatus.UR = false;
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
                     controllers_[0].ControlStatus.DirectionalStatus.DR = true;
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
                    controllers_[0].ControlStatus.DirectionalStatus.DL = true;
                }));
            }
        }

        //==============================================================================

        internal Command downLeftReleaseCommand;
        public ICommand DownLeftReleaseCommand
        {
            get
            {
                return (ICommand)downLeftCommand ?? (downLeftCommand = new Command(() =>
                 {
                     controllers_[0].ControlStatus.DirectionalStatus.DL = false;
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
                     controllers_[0].ControlStatus.DirectionalStatus.UL = true;
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
                     controllers_[0].ControlStatus.DirectionalStatus.UL = false;
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
                     controllers_[0].ControlStatus.Jump = true;
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
                     controllers_[0].ControlStatus.Jump = false;
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
                     controllers_[0].ControlStatus.Attack = true;
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
                     controllers_[0].ControlStatus.Attack = false;
                 }));
            }
        }
    }
}