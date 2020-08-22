using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Valkyrie.Controls
{

    //=============================================================

    public class ControlStatus : INotifyPropertyChanged
    {
        internal bool jump_ = false;
        public bool Jump
        {
            get => jump_;
            set => jump_ = value;
        }

        //---------------------------------------------------

        internal bool attack_ = false;
        public bool Attack
        {
            get => attack_;
            set => attack_ = value;
        }

        //-------------------------------------------------

        internal DirectionalStatus directional_ = new DirectionalStatus();

        public DirectionalStatus DirectionalStatus
        {
            get => directional_;

            //set => directional_ = value;

            set
            {
                directional_ = value;
                RaisePropertyChanged();
            }
        }

        //============================================================

        // property changed event handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
