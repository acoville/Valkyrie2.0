/*==========================================================
 * 
 *  Valkyrie v2.0
 *  Game Logic Test Project
 *  Character class unit tests
 *  
 *  author: adam.coville@gmail.com
 *  maintainer: 
 * 
 * ========================================================*/

using NUnit.Framework;
using Valkryie.GL;
using Valkyrie.GL;

namespace Valkyrie.GL.Test
{
    public class CharacterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //==========================================================================

        /*-------------------------------------
         * 
         *  Sanity check to make sure 
         *  the class is still instantiable
         * 
         * -----------------------------------*/

        [Test]
        [Category("Character")]
        public void DefaultConstructorTest()
        {
            Character SUT = new Character();
            Assert.Pass();
        }

        //==========================================================================

        /*-------------------------------------------------
         * 
         * Tests to ensure character dies when hit points
         * are reduced to 0
         * 
         * ----------------------------------------------*/

        [Test]
        [Category("Character")]
        [Category("Combat")]
        [Category("HP")]
        [Category("Death")]
        public void DeathByHitPointsTest()
        {
            Character SUT = new Character();
            Assert.IsTrue(!SUT.Dead);

            SUT.HP -= 101;
            Assert.IsTrue(SUT.Dead);
        }

        //==========================================================================

        /*------------------------------------
         * 
         * Ensure that commands cannot
         * be given to a dead character
         * 
         * ---------------------------------*/

        [Test]
        [Category("Character")]
        [Category("Combat")]
        [Category("HP")]
        [Category("Death")]
        public void DeadCharacterCannotRecieveCommands()
        {
            Character SUT = new Character();
            string finalWords = SUT.Command;

            SUT.HP -= 101;
            Assert.IsTrue(SUT.Dead);

            SUT.Command = "happy";
            Assert.AreEqual(SUT.Command, finalWords);
        }

        //==========================================================================

        /*----------------------------------
         * 
         *  Ensure that a revived / respawned
         *  character is no longer dead and 
         *  can recieve commands
         * 
         * --------------------------------*/

        [Test]
        [Category("Character")]
        [Category("Combat")]
        [Category("HP")]
        [Category("Death")]
        public void RevivedCharacterIsAliveAgain()
        {
            Character SUT = new Character();
            SUT.HP -= 101;

            Assert.IsTrue(SUT.Dead);

            SUT.HP += 10;
            Assert.IsFalse(SUT.Dead);
        }

        //==========================================================================

        /*--------------------------------------
         * 
         *  Ensure that a revived / respawned
         *  character is no longer dead and 
         *  can recieve commands
         * 
         * ------------------------------------*/

        [Test]
        [Category("Character")]
        [Category("Combat")]
        [Category("HP")]
        [Category("Death")]
        public void RevivedCharacterReceivesCommands()
        {
            Character SUT = new Character();
            string finalWords = SUT.Command;

            SUT.HP -= 101;
            Assert.IsTrue(SUT.Dead);

            SUT.Command = "happy";
            Assert.AreEqual(SUT.Command, finalWords);

            SUT.HP += 10;
            Assert.IsFalse(SUT.Dead);

            SUT.Command = "happy";
            Assert.AreEqual(SUT.Command, "happy");
        }


        //==========================================================================

        /*----------------------------------
         * 
         * Test to make sure that HP does 
         * not go below 0
         * 
         * --------------------------------*/

        [Test]
        [Category("Character")]
        [Category("HP")]
        [Category("Combat")]
        public void HitPointsBoundsLowTest()
        {
            Character SUT = new Character();
            SUT.HP -= 101;

            Assert.IsTrue(SUT.HP >= 0);
        }

        //==========================================================================

        /*------------------------------------------
         * 
         * Test to make sure that overhealing
         * does not exceed max hitpoints
         * 
         * maxHitPoints is not part of the 
         * public character API however
         * I know by default it will be 100
         * 
         * ---------------------------------------*/

        [Test]
        [Category("Character")]
        [Category("HP")]
        [Category("Combat")]
        public void HitPointsBoundsHighTest()
        {
            Character SUT = new Character();

            SUT.HP += 100;

            Assert.IsTrue(SUT.HP <= 100);
        }

        //===========================================================

        /*-----------------------------------------
         * 
         *  Lower bound check on player's stamina
         * 
         * by default, starting stamina should be 
         * 100
         * 
         * --------------------------------------*/

        [Test]
        [Category("Character")]
        [Category("Stamina")]
        [Category("Combat")]
        public void StaminaLowBoundTest()
        {
            Character SUT = new Character();
            SUT.SP -= 110;

            Assert.AreEqual(SUT.SP, 0);
        }

        //==============================================================

        /*------------------------------------
         * 
         * Upper bound check on stamina
         * 
         * ----------------------------------*/

        [Test]
        [Category("Character")]
        [Category("Stamina")]
        [Category("Combat")]
        public void StaminaUpperBoundTest()
        {
            Character SUT = new Character();
            int startingStamina = SUT.SP;
            SUT.SP += 50;

            Assert.AreEqual(SUT.SP, startingStamina);
        }

        //=========================================================

        /*-----------------------------
         * 
         * Check on parry ability
         * 
         * ---------------------------*/

        [Test]
        [Category("Character")]
        [Category("Combat")]
        [Category("Skill")]
        public void ParrySkillCheck()
        {
            Character SUT = new Character();
            SUT.Parry.Level = 10.0;

            Assert.AreEqual(SUT.Parry.Level, 10.0);
        }

        //=============================================================

        /*-----------------------------
         * 
         * Check of MitigateDamage
         * using melee
         * 
         * --------------------------*/

        [Test]
        [Category("Character")]
        [Category("Combat")]
        public void MitigateMeleeDamage()
        {
            Character Victim = new Character();

            int startingHP = Victim.HP;

            Victim.TakeDamage(5, DamageType.melee);

            int endingHP = Victim.HP;

            Assert.AreNotEqual(startingHP, endingHP);
        }
    }
}