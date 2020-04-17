using System;
using System.Collections.Generic;
using System.Text;
using Valkyrie.GL;

namespace Valkryie.GL
{
    public class Attack
    {
        internal GLCharacter attacker_;
        public GLCharacter Attacker 
        {
            get
            {
                return attacker_;
            }
            set
            {
                attacker_ = value;
            }
        }

        //==========================================

        public Skill AttackSkill;

        //==========================================
        
        internal GLCharacter defender_;
        public GLCharacter Defender
        {
            get
            {
                return defender_;
            }
            set
            {
                defender_ = value;
            }
        }

        //===========================================

        internal Weapon weapon_;
        public Weapon AttackWeapon 
        
        { 
            get
            {
                return weapon_;
            }
            set
            {
                weapon_ = value;
            }
        }

        //=============================================

        // constructor

        public Attack(GLCharacter attacker,
                      GLCharacter defender,
                      Weapon attackingWeapon)
        {
            Attacker = attacker;
            Defender = defender;
            AttackWeapon = attackingWeapon;
        }
    }
}
