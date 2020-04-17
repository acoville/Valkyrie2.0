/*======================================================
 * 
 *  Valkyrie
 *  v2.0 alpha
 *  
 *  The purpose of this class is to resolve combat 
 *  between two or more actors
 * 
 * 
 * ===================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using Valkyrie.GL;

namespace Valkryie.GL
{
    public class DungeonMaster
    {
        //======================================================

        /*----------------------------------------
         * 
         *  The DM is strictly resolving HP, 
         *  Stamina, mana or whatever is going 
         *  to be up to the Character class to 
         *  resolve
         * 
         * -------------------------------------*/

        public void ResolveCombat(Attack attack)
        {
            // determine how much damage the attack causes

            GLCharacter attacker = attack.Attacker;
            Skill attackSkill = attack.AttackSkill;

            int UnmitigatedDamage = attack.AttackWeapon.Damage;
            UnmitigatedDamage += (int)attackSkill.Level;

            // apply RNG here

            DamageType type = attack.AttackWeapon.DamageType;

            //--------------------------------------------------------------

            // The Character class will now perform any 
            // mitigation and deduct HP

            GLCharacter defender = attack.Defender;
            defender.TakeDamage(UnmitigatedDamage, type);
        }
    }
}
