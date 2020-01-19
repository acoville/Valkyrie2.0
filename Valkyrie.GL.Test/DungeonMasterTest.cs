using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Valkryie.GL;

namespace Valkyrie.GL.Test
{
    class DungeonMasterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //====================================================

        [Test]
        [Category("Dungeon Master")]
        public void SanityCheck()
        {
            var DM = new DungeonMaster();
            Assert.Pass();
        }

        //=====================================================

        /*-------------------------------------
         * 
         * Test to make sure we can actually 
         * launch and resolve an attack using 
         * the DM class
         * 
         * ----------------------------------*/

        [Test]
        [Category("Combat")]
        [Category("Dungeon Master")]
        public void AttackTest()
        {
            Weapon sword = new Weapon();
            sword.Damage = 5;

            Skill swordplay = new Skill("swordplay", 10);

            var Attacker = new Character();
            Attacker.MeleeWeapon = sword;

            var Defender = new Character();
            Defender.MeleeWeapon = sword;

            //---------------------------------------

            Attack stabbinating = new Attack(Attacker, Defender, sword);
            stabbinating.AttackSkill = swordplay;

            var SUT = new DungeonMaster();

            int startingHP = Defender.HP;

            SUT.ResolveCombat(stabbinating);

            int endingHP = Defender.HP;

            Assert.Less(endingHP, startingHP);
        }
    }
}
