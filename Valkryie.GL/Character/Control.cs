/*============================================================
 * 
 *  Valkyrie v2.0
 *  Game Logic .NET standard library
 *  Character class
 *  Control characteristics
 *  
 *  the properties here store unique command information for 
 *  each character in-play, which will trigger attacks, jumps, 
 *  motion or other states required
 *  
 *  author: adam.coville@gmail.com
 *  maintainer:
 * 
 * =========================================================*/

using System;

namespace Valkyrie.GL
{
    public partial class Character
    {
        //=========================================================

        private String name_ = "UNKNOWN";
        public String Name
        {
            get
            {
                return name_;
            }

            set
            {
                name_ = value;
            }
        }

        //=========================================================

        /*-------------------------------------
         * 
         *  Variables governing interaction 
         *  with computer AI / player control
         * 
         * ----------------------------------*/

        private int team_ = 0;
        public int Team
        {
            get
            {
                return team_;
            }
            set
            {
                team_ = value;
            }
        }

        //=========================================================

        /*-------------------------------
         * 
         *  Control Variables
         * 
         * ----------------------------*/

        private String currentCommand_ = "standing";
        public String Command
        {
            get
            {
                return currentCommand_;
            }
            set
            {
                if (!Dead)
                {
                    currentCommand_ = value;
                }
            }
        }
    }
}