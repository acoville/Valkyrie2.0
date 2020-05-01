/*============================================================
 * 
 *  Valkyrie v2.0
 *  Game Logic .NET standard library
 *  Character class
 *  Motion characteristics
 *  
 *  author: adam.coville@gmail.com
 *  maintainer:
 * 
 * =========================================================*/

namespace Valkyrie.GL
{
    public partial class GLCharacter
    {
        //=====================================================

        /*-------------------------------
         *  
         *  Jump logic
         * 
         * ----------------------------*/

        private int maxJumps_ = 1;
        public int MaxJumps
        {
            get => maxJumps_;
            set => maxJumps_ = value;
        }

        //--------------------------------------

        private int currentJump_ = 0;
        public int CurrentJumps
        {
            get => currentJump_;

            set
            {
                if (value >= 0 && value < MaxJumps)
                {
                    currentJump_ = value;
                }
            }
        }

        //========================================================

        /*-------------------------------------------
         * 
         * Properties governing the character's
         * vertical motion
         * 
         * ----------------------------------------*/

        private double ySpeed_ = 0.0;
        public double ySpeed
        {
            get => ySpeed_;
            set => ySpeed_ = value;
        }

        //---------------------------------------
        public double yAccelerationRate { get; set; }

        //---------------------------------------

        public void Acclerate_Y()
        {
            ySpeed += yAccelerationRate;
        }

        //=========================================================

        /*-------------------------------------------
         * 
         *  Properties governing the character's
         *  lateral motion (ie running speed)
         *  and gives smooth acceleration from 
         *  start to full speed
         * 
         *  This propery may be modified by 
         *  the player's stamina
         * 
         * ----------------------------------------*/

        internal int max_X_Speed_ = 20;
        public int Max_X_Speed
        {
            get
            {
                return max_X_Speed_;
            }

            set
            {
                max_X_Speed_ = value;
            }
        }

        //---------------------------------------

        internal double max_X_AcclerationRate_ = 20.0;

        //---------------------------------------

        internal double xAccelRate_ = 0.0;
        public double xAccelerationRate
        {
            get
            {
                return xAccelRate_;
            }
            set
            {
                if (value <= max_X_AcclerationRate_)
                {
                    xAccelRate_ = value;
                }
            }
        }

        //---------------------------------------

        internal double xSpeed_ = 0.0;
        public double xSpeed
        {
            get
            {
                return xSpeed_;
            }
            set
            {
                if (value <= Max_X_Speed)
                {
                    xSpeed_ = value;
                }
            }
        }

        //---------------------------------------

        public void Accelerate_X()
        {
            if (xSpeed + xAccelerationRate >= Max_X_Speed)
            {
                xSpeed = Max_X_Speed;
            }
            else
            {
                xSpeed += xAccelerationRate;
            }
        }

        //=========================================================

        /*----------------------------------------
         * 
         *  This property governs weather the 
         *  character will be accelerating 
         *  towards the ground in the 
         *  GamePageViewModel's EvaluateMotion()
         *  loop.
         * 
         * -------------------------------------*/

        internal bool falling_ = false;
        public bool Falling
        {
            get
            {
                return falling_;
            }

            set
            {
                falling_ = value;

                // if falling is true

                if (value == true)
                {
                    standing_ = false;
                }

                // if falling is false

                else
                {
                    CurrentJumps = 0;
                    ySpeed = 0;
                    yAccelerationRate = 0;
                }
            }
        }

        //========================================================

        internal bool standing_ = true;
        public bool Standing
        {
            get
            {
                return standing_;
            }

            set
            {
                // notifies the graphics to alter the sprite

                standing_ = value;

                // stops falling during GPVM.EvaluateMotion()

                if (value == true)
                    falling_ = false;
            }
        }

        //========================================================

        internal Direction facing_ = Direction.right;
        public Direction Facing
        {
            get
            {
                return facing_;
            }

            set
            {
                facing_ = value;
            }
        }
    }
}
