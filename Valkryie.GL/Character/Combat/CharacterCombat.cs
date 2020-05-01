/*============================================================
 * 
 *  Valkyrie v2.0
 *  Game Logic .NET standard library
 *  Character class
 *  Combat Characteristics
 *  
 *  author: adam.coville@gmail.com
 *  maintainer:
 * 
 * =========================================================*/

using Valkryie.GL;

namespace Valkyrie.GL
{
    public partial class GLCharacter
    {
        //==========================================================

        internal bool dead_ = false;

        public bool Dead
        {
            get
            {
                return dead_;
            }

            set
            {
                // will lock controls until respawned.

                dead_ = value;

                // updated command will notify the graphics engine
                // to update the sprite

                if (value == true)
                {
                    Command = "dead";
                }
            }
        }

        //========================================================

        /*----------------------------------
         * 
         *  Stamina Recovery occurs 
         *  naturally whenever the player
         *  is not moving.
         * 

        internal int staminaRecoveryRate_ = 5;
        internal void RecoverStamina()
        {
            SP += staminaRecoveryRate_;
        }
         * -------------------------------*/

        //========================================================================

        /*-------------------------------------
         * 
         *  Recieves incoming damage dealt,
         *  
         * 

        public void TakeDamage(int unmitigatedDamage, DamageType type)
        {
            int mitigatedDamage = MitigateDamage(unmitigatedDamage, type);
            HP -= mitigatedDamage;
        }
         * -----------------------------------*/

        //=========================================================================

        /*----------------------------------------
         * 
         * Weapons.. I am certain I am going 
         * to want to further encapsulate this
         * in an inventory system soon.
         * 

        internal Weapon meleeWeapon_;
        public Weapon MeleeWeapon 
        {
            get
            {
                return meleeWeapon_;
            }
            set
            {
                meleeWeapon_ = value;
            }
        }

        //---------------------------------------

        internal Weapon rangedWeapon_;
        public Weapon RangedWeapon
        {
            get
            {
                return rangedWeapon_;
            }
            set
            {
                rangedWeapon_ = value;
            }
        }
         * --------------------------------------*/

        //=========================================================================

        /*-----------------------------------
         * 
         *  This will take into effect 
         *  mitigations such as buffs, 
         *  armor, dodge, parry and 
         *  damage type
         * 

        internal int MitigateDamage(int unmitigatedDamage, DamageType type)
        {
            int modifiedDamage = 0;

            switch (type)
            {
                case (DamageType.melee):
                {
                    modifiedDamage = MitigateMeleeDamage(unmitigatedDamage);
                    break;
                }

                //-----------------------------------------------

                case (DamageType.piercing):
                {
                    modifiedDamage = MitigatePiercingDamage(unmitigatedDamage);
                    break;
                }

                //-------------------------------------------------

                default:
                {
                    modifiedDamage = unmitigatedDamage;
                    break;
                }
            }

            return modifiedDamage;
        }
         * --------------------------------*/
    }
}