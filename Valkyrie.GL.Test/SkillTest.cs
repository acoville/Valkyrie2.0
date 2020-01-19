/*===========================================================
 * 
 *  Valkyrie v2.0
 *  Game Logic Test Project
 *  Skill class unit test
 *  
 *  author: adam.coville@gmail.com
 *  maintainer: 
 * 
 * =======================================================*/

using NUnit.Framework;
using Valkyrie.GL;

namespace Valkyrie.GL.Test
{
    public class SkillTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //===============================================================

        /*-----------------------------
         * 
         *  Sanity Check to make sure I 
         *  can construct one 
         * 
         * ----------------------------*/

        [Test]
        [Category("Skill")]
        public void ConstructorTest()
        {
            Skill pottery = new Skill("pottery", 5);
            Assert.Pass();
        }

        //=============================================================

        /*-----------------------------
         * 
         * Simple skill check
         * 
         * ----------------------------*/

        [Test]
        [Category("Skill")]
        public void SkillCheckPassing()
        {
            Skill BasketWeaving = new Skill("basket weaving", 5);
            Assert.IsTrue(BasketWeaving.SkillCheck(3));
        }

        //--------------------------------------------------

        [Test]
        [Category("Skill")]
        public void SkillCheckFailing()
        {
            Skill BasketWeaving = new Skill("basket weaving", 5);
            Assert.IsFalse(BasketWeaving.SkillCheck(10));
        }

        //=========================================================

        [Test]
        [Category("Skill")]
        public void SkillEqualityTrueCheck()
        {
            Skill Fencing1 = new Skill("fencing", 1.0);
            Skill Fencing2 = new Skill("fencing", 1.0);

            Assert.IsTrue(Fencing1 == Fencing2);
        }

        //===========================================================

        [Test]
        [Category("Skill")]
        public void SkillEqualityFalseCheck()
        {
            Skill Fencing1 = new Skill("fencing", 1.0);
            Skill Fencing2 = new Skill("fencing", 2.0);

            Assert.IsFalse(Fencing1 == Fencing2);
        }

        //===========================================================

        [Test]
        [Category("Skill")]
        public void UnequalHashesCheck()
        {
            Skill Fencing1 = new Skill("fencing", 1.0);
            Skill Fencing2 = new Skill("fencing", 2.0);

            Assert.IsFalse(Fencing1.GetHashCode() == Fencing2.GetHashCode());
        }

        //=============================================================

        [Test]
        [Category("Skill")]
        public void EqualHashesCheck()
        {
            Skill Fencing1 = new Skill("fencing", 1.0);
            Skill Fencing2 = Fencing1;

            Assert.IsTrue(Fencing1.GetHashCode() == Fencing2.GetHashCode());
        }

        //================================================================

        [Test]
        [Category("Skill")]
        public void GreaterThanOperatorTrue()
        {
            Skill Fencing1 = new Skill("fencing", 2.0);
            Skill Fencing2 = new Skill("fencing", 1.0);

            Assert.IsTrue(Fencing1 > Fencing2);
        }

        //================================================================

        [Test]
        [Category("Skill")]
        public void GreaterThanOperatorFalse()
        {
            Skill Fencing1 = new Skill("fencing", 1.0);
            Skill Fencing2 = new Skill("fencing", 2.0);

            Assert.IsFalse(Fencing1 > Fencing2);
        }

        //==================================================================

        [Test]
        [Category("Skill")]
        public void SkillLevelUpTest()
        {
            Skill HoldingBreath = new Skill("holding breath", 1.0);
            HoldingBreath.LevelUp();

            Assert.AreNotEqual(HoldingBreath.Level, 1.0);
        }
    }
}
