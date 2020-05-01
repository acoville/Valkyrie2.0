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
    class Controller : IController
    {
        internal DateTime t1 = DateTime.Now;
        internal DateTime t2;
        internal TimeSpan timeSinceLastInput = TimeSpan.FromSeconds(1.0);

        // we will go with a reset timer of 1/4 second and see how that feels

        internal TimeSpan resetTime = TimeSpan.FromMilliseconds(250);

        internal string input_ = "";
        
        //=======================================================================

        /*----------------------------------------------------------------
         * 
         * The Input property updates the controller's input string. 
         * This string could be a simple 1-button press like 'A' 
         * will ouput the command Jump or a complex combination 
         * like the hadoken with 3 directionals and an attack done in 
         * order and within a certain time window. 
         * 
         * The input window is staring at 250ms before the string resets.
         * 
         * -------------------------------------------------------------*/

        public string Input
        {
            get => input_;

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

                //-- evaulate the string to see if it matches a command


            }
        }

        //======================================================================

        public Command SendCommand()
        {
            throw new NotImplementedException();
        }
    }
}
