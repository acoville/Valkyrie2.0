
namespace Valkyrie.GL
{
    public partial class Character
    {
        //==========================================================

        /*-----------------------------------
         * 
         * Hit Points or HP 
         * 
         * represent the player's life 
         * if HP falls to 0 they die
         * 
         * --------------------------------*/

        internal int maxHitPoints_ = 100;
        public int MaxHP
        {
            get
            {
                return maxHitPoints_;
            }

            set
            {
                maxHitPoints_ = value;
            }
        }

        //-----------------------------------------------

        internal int hitPoints_ = 100;
        public int HP
        {
            get
            {
                return hitPoints_;
            }

            set
            {
                //-- check and make sure we are within bounds

                if (value > 0 && value <= MaxHP)
                {
                    hitPoints_ = value;
                    if (Dead)
                    {
                        Dead = false;
                    }
                }

                //-- check and see if the change in HP kills character

                if (value <= 0)
                {
                    hitPoints_ = 0;
                    Dead = true;
                }
            }
        }

        //========================================================================

        /*---------------------------------------------
         * 
         *  Stamina Points or SP 
         *  
         *  these govern how well the player's 
         *  character is able to move and attack.
         *  A high stamina would mean the player is
         *  rested and ready, a low stamina would 
         *  mean they are dragging their feet out of
         *  breath, have lower attack speed, lower
         *  movement speed, etc..
         * 
         * ------------------------------------------*/

        internal int stamina_ = 100;
        public int SP
        {
            get
            {
                return stamina_;
            }
            set
            {
                if (value > maxStamina_)
                {
                    stamina_ = maxStamina_;
                    return;
                }

                if (value < 0)
                {
                    stamina_ = 0;
                    return;
                }

                stamina_ = value;
            }
        }

        //=========================================================

        /*---------------------------------
         * 
         *  Maximum Stamina governs how 
         *  much SP may be regained
         * 
         * ------------------------------*/

        internal int maxStamina_ = 100;

        public int MaxSP
        {
            get
            {
                return maxStamina_;
            }

            set
            {
                maxStamina_ = value;
            }
        }

        //==========================================================

        /*------------------------------
         * 
         * Melee Skills
         * Parry
         * 
         * ---------------------------*/

        internal Skill dodge_ = new Skill("dodge", 1);

        public Skill Dodge
        {
            get { return dodge_; }
        }

        //==========================================================

        /*-------------------------
         *
         * Melee Skills
         * Dodge
         * 
         * -----------------------*/

        internal Skill parry_ = new Skill("parry", 1);
        public Skill Parry
        {
            get { return parry_; }
        }

        //==========================================================

        /*-----------------------
         * 
         * Melee Skills
         * Block
         * 
         * ---------------------*/
        
        internal Skill block_ = new Skill("block", 1);
        public Skill Block
        {
            get { return block_; }
        }

        //===============================================================

        /*-------------------------
         * 
         * Melee Skills
         * Attack
         * 

        internal Skill 
         * ----------------------*/
    }
}