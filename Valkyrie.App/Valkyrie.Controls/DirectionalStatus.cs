using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.Controls
{
    public class DirectionalStatus
    {
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

        //=================================================================
        public override bool Equals(object obj)
        {
            return obj is DirectionalStatus status &&
                   down_ == status.down_ &&
                   D == status.D &&
                   left_ == status.left_ &&
                   L == status.L &&
                   right_ == status.right_ &&
                   R == status.R &&
                   up_ == status.up_ &&
                   U == status.U &&
                   dr_ == status.dr_ &&
                   DR == status.DR &&
                   dl_ == status.dl_ &&
                   DL == status.DL &&
                   ul_ == status.ul_ &&
                   UL == status.UL &&
                   ur_ == status.ur_ &&
                   UR == status.UR;
        }

        //=================================================================

        public override int GetHashCode()
        {
            int hashCode = 880890105;
            hashCode = hashCode * -1521134295 + down_.GetHashCode();
            hashCode = hashCode * -1521134295 + D.GetHashCode();
            hashCode = hashCode * -1521134295 + left_.GetHashCode();
            hashCode = hashCode * -1521134295 + L.GetHashCode();
            hashCode = hashCode * -1521134295 + right_.GetHashCode();
            hashCode = hashCode * -1521134295 + R.GetHashCode();
            hashCode = hashCode * -1521134295 + up_.GetHashCode();
            hashCode = hashCode * -1521134295 + U.GetHashCode();
            hashCode = hashCode * -1521134295 + dr_.GetHashCode();
            hashCode = hashCode * -1521134295 + DR.GetHashCode();
            hashCode = hashCode * -1521134295 + dl_.GetHashCode();
            hashCode = hashCode * -1521134295 + DL.GetHashCode();
            hashCode = hashCode * -1521134295 + ul_.GetHashCode();
            hashCode = hashCode * -1521134295 + UL.GetHashCode();
            hashCode = hashCode * -1521134295 + ur_.GetHashCode();
            hashCode = hashCode * -1521134295 + UR.GetHashCode();
            return hashCode;
        }

        //=====================================================================

        /*---------------------------------
         * 
         * Operators
         * 
         * ------------------------------*/

        public static bool operator == (DirectionalStatus d1, DirectionalStatus d2)
        {
            bool U = (d1.U == d2.U);
            bool UR = (d1.UR == d2.UR);
            bool R = (d1.R == d2.R);
            bool DR = (d1.DR == d2.DR);
            bool D = (d1.D == d2.D);
            bool DL = (d1.DL == d2.DL);
            bool L = (d1.L == d2.L);
            bool UL = (d1.UL == d2.UL);

            return (U && UR && R && DR && D && DL && L && UL);
        }

        //-------------------------------------------------------------

        public static bool operator != (DirectionalStatus d1, DirectionalStatus d2)
        {
            bool U = (d1.U == d2.U);
            bool UR = (d1.UR == d2.UR);
            bool R = (d1.R == d2.R);
            bool DR = (d1.DR == d2.DR);
            bool D = (d1.D == d2.D);
            bool DL = (d1.DL == d2.DL);
            bool L = (d1.L == d2.L);
            bool UL = (d1.UL == d2.UL);

            return (U || UR || R || DR || D || DL || L || UL);
        }
    }
}
