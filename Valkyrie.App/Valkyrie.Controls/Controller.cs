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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Valkyrie.Controls
{
    public class Controller : IController, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //-- internal timer data relating to input recognition

        internal DateTime t1 = DateTime.Now;
        internal DateTime t2;
        internal TimeSpan timeSinceLastInput = TimeSpan.FromSeconds(1.0);
        internal TimeSpan resetTime = TimeSpan.FromMilliseconds(250);
        internal TimeSpan simultaneousPressTime = TimeSpan.FromMilliseconds(50);

        //=======================================================================

        /*---------------------------------------
         * 
         * Property which should be bound to 
         * matching GPVM.Actor 
         * 
         * -------------------------------------*/

        internal ControlStatus status_;
        public ControlStatus ControlStatus
        {
            get => status_;

            set
            {
                status_ = value;
                EvaluateControlStatus();
                RaisePropertyChanged();
            }
        }

        //=======================================================================

        internal void EvaluateControlStatus()
        {
            Input = "A";
            
            /*
            if(status_.Jump)
            {
            }
             */
        }

        //=======================================================================

        /*-----------------------------------------
         * 
         * Default Constructor
         * 
         * default input set
         * 
         * --------------------------------------*/

        public Controller()
        {
            status_ = new ControlStatus();

            Commands = new List<string>
            {
                // directionals

                "UP",
                "UR",
                "R",
                "DR",
                "DN",
                "DL",
                "L",
                "UL",

                // actions

                "B",
                "A"
            };
        }

        //=======================================================================

        /*------------------------------------------
         * 
         * The commands list contains 
         * all the input sequences which should 
         * result in an action, for instance
         * 
         * from classic Street Fighter: Hadoken
         * D, DR, R + punch
         * D, DL, L + punch
         * 
         * -------------------------------------*/

        internal List<string> commands_;
        public List<string> Commands
        {
            get => commands_;
            set => commands_ = value;
        }

        //======================================================================

        /*------------------------------------------
         * 
         * Attempts to find a match in the 
         * list of command strings. 
         *
         * Complexity: linear time
         * 
         * ---------------------------------------*/

        public bool ParseCommand()
        {
            foreach (var command in commands_)
            {
                if (input_ == command)
                {
                    return true;
                }
            }

            return false;
        }

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

        internal string input_ = "";

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
                    // have to put this if for an empty string here, or else 
                    // the first input will get a leading ", "

                    if(input_ == "")
                    {
                        input_ += value;
                    }

                    else
                    {
                        // if the press is within a window of 50ms, then it is
                        // considered a simultaneous press

                        if(timeSinceLastInput <= simultaneousPressTime)
                        {
                            input_ += " + " + value;
                        }

                        // otherwise, it is considered a sequential press
                        
                        else
                        {
                            input_ += ", " + value;
                        }
                    }

                    // either way, we now need to reset the last input timer

                    t1 = DateTime.Now;
                    timeSinceLastInput = TimeSpan.FromSeconds(0.0);
                }

                //--------------------------

                // see if it matches any of the input strings 

                if(ParseCommand())
                {
                    // send the command

                    // flush the input string buffer

                    
                }
            }
        }

        //======================================================================

        /*---------------------------------------
         * 
         * Raise Property Changed Event Handler
         * 
         * -------------------------------------*/

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
