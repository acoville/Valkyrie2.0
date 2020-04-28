/*===================================================================
 * 
 * Valkyrie.Controls 
 * 
 * Virtual Gamepad
 * 
 * takes input from the soft-keys on screen and attempts to parse
 * them into valid commands
 * 
 * ================================================================*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.Controls
{
    class VPadController : IController
    {
        internal DateTime t1 = DateTime.Now;
        internal DateTime t2;
        internal TimeSpan timeSinceLastInput = TimeSpan.FromSeconds(1.0);

        // we will go with a reset timer of 1/4 second and see how that feels

        internal TimeSpan resetTime = TimeSpan.FromMilliseconds(250);

        internal string input_ = "";
        
        //=======================================================================

        /*---------------------------------------
         * 
         * 
         * 
         * -------------------------------------*/

        public string Input
        {
            set
            {
                // how much time has elapsed since the last input?

                t2 = DateTime.Now;
                timeSinceLastInput = t2 - t1;

                //-- too much time has elapsed since the last input, reset
                // the string

                if (timeSinceLastInput >= resetTime)
                {
                    timeSinceLastInput = TimeSpan.FromSeconds(0.0);
                    t1 = DateTime.Now;

                    input_ = value;
                }

                //-- we are still within the window where the string can be 
                // modified

                else
                {
                    input_ += value;
                    timeSinceLastInput = TimeSpan.FromSeconds(0.0);
                }
            }
        }

        //======================================================================
        public Command SendCommand()
        {
            throw new NotImplementedException();
        }
    }
}
