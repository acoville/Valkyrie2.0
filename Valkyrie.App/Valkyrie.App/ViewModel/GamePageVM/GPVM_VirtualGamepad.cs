

using System.ComponentModel;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        public void UpCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.U = true;
        }

        //-------------------------------

        public void UpReleaseCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.U = true;
        }

        //===============================================

        public void UpRightCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.UR = true;
        }

        //-------------------------------

        public void UpRightReleaseCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.UR = false;
        }

        //===============================================

        public void RightCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.R = true;
        }

        //-------------------------------
        public void RightReleaseCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.R = false;
        }

        //===============================================

        public void DownRightCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.DR = true;
        }

        //-------------------------------
        public void DownRightReleaseCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.DR = false;
        }

        //===============================================

        public void DownCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.D = true;
        }

        //-------------------------------

        public void DownReleaseCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.D = false;
        }

        //================================================

        public void DownLeftCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.DL = true;
        }

        //-------------------------------

        public void DownLeftReleaseCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.DL = false;
        }

        //===============================================

        public void LeftCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.L = true;
        }

        //-------------------------------

        public void LeftReleaseCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.L = false;
        }

        //===============================================

        public void UpLeftCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.UL = true;
        }

        //-------------------------------

        public void UpLeftReleaseCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.UL = false;
        }

        //==============================================

        public void NullCommand()
        {
            controllers_[0].ControlStatus.DirectionalStatus.NullDirection();
        }

        //=====================================================

        public void BCommand()
        {
            controllers_[0].ControlStatus.Attack = true;
        }

        //-------------------------------

        public void BReleaseCommand()
        {
            controllers_[0].ControlStatus.Attack = false;
        }

        //===================================================

        public void ACommand()
        {
            controllers_[0].ControlStatus.Jump = true;
        }

        //-------------------------------
        public void AReleaseCommand()
        {
            controllers_[0].ControlStatus.Jump = false;
        }
    }
}