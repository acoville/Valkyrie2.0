/*================================================================
 * 
 *  A skill represents the character's aptitude at a particular
 *  task. Melee skills for example, like parry govern the success
 *  rate of a parry. Succeeding at it increases the skill level. 
 *  In melee combat, successful attacks and defenses are decided
 *  by opposing skill checks.
 *  
 *  Skills only ever increase in 0 and must start > 0. 
 * 
 * ==============================================================*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.GL
{
    public class Skill
    {
        internal String title_;
        public String Title 
        {
            get
            {
                return title_;
            }
            set
            {
                title_ = value;
            }
        }

        //=======================================================

        internal double level_ = 1;
        public double Level
        {
            get
            {
                return level_;
            }
            set
            {
                // skills only ever increase in value

                if(value > 0 && value > level_)
                {
                    level_ = value;
                }
            }
        }

        //=======================================================

        /*-----------------------
         * 
         * Constructors 
         * 
         * --------------------*/

        public Skill(String name, double level)
        {
            Title = name;
            Level = level;
        }

        //========================================================

        /*-------------------------------------
         *  
         *  Increments the skill level by 1
         * 
         * ----------------------------------*/

        public void LevelUp()
        {
            Level += 1.0;
        }

        //============================================================

        /*-------------------------------------------------
         * 
         *  Opposed Skill check
         *  
         *  this static method will return a 
         *  ratio of the skill levels of 2 
         *  skill instances. This will be used often 
         *  in example, in melee combat. 
         *  
         *  In a case where 2 fighters have equal skill
         *  neither will have an advantage
         * 
         * ----------------------------------------------*/
        
        static double SkillRatio(Skill A, Skill B)
        {
            double ratio = 0.0;

            ratio = A.Level / B.Level;

            return ratio;
        }

        //==============================================================



        //==============================================================

        public bool SkillCheck(double target)
        {
            return (Level >= target) ? true : false;
        }

        //==============================================================

        /*-------------------------------------
         * 
         * Operators useful for skill checks
         * 
         * -----------------------------------*/

        public static bool operator > (Skill A, Skill B)
        {
            return (A.Level > B.Level);
        }

        public static bool operator < (Skill A, Skill B)
        {
            return (A.Level < B.Level);
        }

        //----------------------------------------------

        /*-------------------------------------
         * 
         * if they are < 1 skill level in 
         * difference, we will treat them as 
         * equals
         * 
         * ----------------------------------*/

        public static bool operator == (Skill A, Skill B)
        {
            return ((int)A.Level == (int)B.Level);
        }

        public static bool operator != (Skill A, Skill B)
        {
            return ((int)A.Level != (int)B.Level);
        }

        //=============================================================

        /*-----------------------------------
         * 
         * Microsoft recommends overloading 
         * .Equals and .GetHash whenever you 
         * overload == 
         * 
         * ---------------------------------*/

        public override bool Equals(object other)
        {
            var otherSkill = other as Skill;

            return ((int)Level == (int)otherSkill.Level);
        }

        //-----------------------------------------------

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521134295 + Title.GetHashCode();
            hashCode = hashCode * -1521134295 + Level.GetHashCode();

            return hashCode;
        }
    }
}
