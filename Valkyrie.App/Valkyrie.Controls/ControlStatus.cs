using System.Collections.Generic;

namespace Valkyrie.Controls
{
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

        //=============================================================
        public override bool Equals(object obj)
        {
            return obj is ControlStatus status &&
                   jump_ == status.jump_ &&
                   Jump == status.Jump &&
                   attack_ == status.attack_ &&
                   Attack == status.Attack &&
                   EqualityComparer<DirectionalStatus>.Default.Equals(directional_, status.directional_) &&
                   EqualityComparer<DirectionalStatus>.Default.Equals(DirectionalStatus, status.DirectionalStatus);
        }

        //===========================================================

        public override int GetHashCode()
        {
            int hashCode = 589789519;
            hashCode = hashCode * -1521134295 + jump_.GetHashCode();
            hashCode = hashCode * -1521134295 + Jump.GetHashCode();
            hashCode = hashCode * -1521134295 + attack_.GetHashCode();
            hashCode = hashCode * -1521134295 + Attack.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<DirectionalStatus>.Default.GetHashCode(directional_);
            hashCode = hashCode * -1521134295 + EqualityComparer<DirectionalStatus>.Default.GetHashCode(DirectionalStatus);
            return hashCode;
        }

        //============================================================

        // equality operators

        public static bool operator == (ControlStatus s1, ControlStatus s2)
        {
            bool Directional = (s1.DirectionalStatus == s2.DirectionalStatus) ? true : false;
            bool A = (s1.Jump == s2.Jump) ? true : false;
            bool B = (s1.Attack == s2.Attack) ? true : false;

            return (Directional && A && B);
        }

        //----------------------------------------------------------

        public static bool operator != (ControlStatus s1, ControlStatus s2)
        {
            bool Directional = (s1.DirectionalStatus == s2.DirectionalStatus) ? true : false;
            bool A = (s1.Jump == s2.Jump) ? true : false;
            bool B = (s2.Attack == s2.Attack) ? true : false;

            return (Directional || A || B);
        }
    }
}
