using System;
using System.Collections.Generic;
using System.Text;
using Valkyrie.GL;

namespace Valkryie.GL
{
    public class Weapon : equipment
    {
        internal DamageType damageType_;
        public DamageType DamageType 
        {
            get
            {
                return damageType_;
            }
            set
            {
                damageType_ = value;
            }
        }

        //================================================

        internal int baseDamage_;
        public int Damage
        {
            get
            {
                return baseDamage_;
            }
            set
            {
                baseDamage_ = value;
            }
        }

        //==============================================

        internal int range_;
        public int Range
        {
            get
            {
                return range_;
            }
            set
            {
                range_ = value;
            }
        }
    }
}
