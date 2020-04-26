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
    public partial class GLCharacter
    {
        //=========================================================

        private String name_ = "UNKNOWN";
        public String Name
        {
            get => name_;
            set => name_ = value;
        }

        //=========================================================

        /*----------------------------
         * Teams
         * 
         * 0: player's team
         * 1: neutral
         * 2 - N: hostiles or 3rd parties
         * 
         * -------------------------*/

        internal int team_ = 1;
        public int Team
        {
            get => team_;
            set => team_ = value;
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