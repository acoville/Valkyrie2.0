using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.Controls
{
    public class DirectionalStatus
    {
        //===================================================

        /*--------------------------------
         * 
         * Default Constructor
         * 

        public DirectionalStatus()
        {
            up_ = false;
            ur_ = false;
            right_ = false;
            dr_ = false;
            down_ = false;
            dl_ = false;
            left_ = false;
            ul_ = false;
        }
         * -----------------------------*/

        //---------------------------------------------------

        internal bool down_ = false;
        public bool D
        {
            get => down_;
            set => down_ = value;
        }

        //--------------------------------------------------

        internal bool left_ = false;
        public bool L
        {
            get => left_;
            set => left_ = value;
        }

        //------------------------------------------------

        internal bool right_ = false;
        public bool R
        {
            get => right_;
            set => right_ = value;
        }

        //----------------------------------------------

        internal bool up_ = false;
        public bool U
        {
            get => up_;
            set => up_ = value;
        }

        //------------------------------------------------

        internal bool dr_ = false;
        public bool DR
        {
            get => dr_;
            set => dr_ = value;
        }

        //------------------------------------------------

        internal bool dl_ = false;
        public bool DL
        {
            get => dl_;
            set => dl_ = value;
        }

        //-----------------------------------------------

        internal bool ul_ = false;
        public bool UL
        {
            get => ul_;
            set => ul_ = value;
        }

        //----------------------------------------------

        internal bool ur_ = false;
        public bool UR
        {
            get => ur_;
            set => ur_ = value;
        }


        //======================================================================

        /*------------------------------------
         * 
         *  Null Direction command
         * 
         * ---------------------------------*/
        public void NullDirection()
        {
            U = false;
            UR = false;
            R = false;
            DR = false;
            D = false;
            DL = false;
            L = false;
            UL = false;
        }
    }
}
