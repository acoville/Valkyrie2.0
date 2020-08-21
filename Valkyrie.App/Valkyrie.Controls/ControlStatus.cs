using System;

namespace Valkyrie.Controls
{

    //=============================================================

    public class ControlStatus
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
            set => directional_ = value;
        }
    }
}
