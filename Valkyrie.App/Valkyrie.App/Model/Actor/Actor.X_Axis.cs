/*-------------------------------
* 
*  X Axis Characteristics
* 
* -------------------------------*/

using System;
using System.Xml.XPath;

namespace Valkyrie.App.Model
{
    public partial class Actor
    {
        internal float x_speed = 0.0f;
        internal float max_x_speed = 25.0f;

        /*-----------------------------------------
         
            ACCELERATION

            default acceleration rate, which might 
            be changed by powerups, etc so it should 
            have a public property

        ------------------------------------------*/

        internal float default_x_acceleration_rate = 1.5f;
        public float DefaultXAccelRate
        {
            get => default_x_acceleration_rate;
            set => default_x_acceleration_rate = value;
        }
 
        internal float max_x_acceleration_rate = 7.5f;
        internal float x_acceleration_rate = 0.0f;

        public float X_Acceleration_Rate
        {
            get => x_acceleration_rate;

            set
            {
                /*
                 *  bounds check the acceleration rate against max x acceleration
                 */

                var absValue = Math.Abs(value);

                if (absValue > max_x_acceleration_rate)
                {
                    if (value < 0)
                    {
                        x_acceleration_rate = (-max_x_acceleration_rate);
                    }
                    else
                    {
                        x_acceleration_rate = max_x_acceleration_rate;
                    }
                }

                // otherwise, just accept the value

                else
                {
                    x_acceleration_rate = value;
                }

                if(x_acceleration_rate == 0 && x_speed == 0)
                {
                    stationary_x_ = true;
                }
            }
        }

        //====================================================================

        /*---------------------------------------
         * 
         * This will be invoked in situations 
         * where there's a colliding object 
         * blocking you from going any further
         * 
         * -------------------------------------*/

        public void StopXAxisMotion()
        {
            x_acceleration_rate = 0.0f;
            x_speed = 0.0f;
        }

        //===================================================================

        /*----------------------------------------
         * 
         * calculates the deltaX for a .Translate()
         * call, based on the current speed in the x 
         * axis and the x_acceleration rate
         * 
         * --------------------------------------*/

        public float Accelerate_X()
        {
            /*
             *      weather we are moving left or right, 
             *      x_speed is still going to be += x_acceleration_rate.
             *      What decides which way the Actor moves is weather 
             *      the acceleration rate is positive or negative. 
             *      All I need to be bounds checking here is x_speed
             *      and x_acceleration_rate.
             */

            x_speed += x_acceleration_rate;

            // bounds check speed

            if(Math.Abs(x_speed) > max_x_speed)
            {
                // negative if going left, positive if going right 

                x_speed = (x_speed < 0) ? (-max_x_speed) : max_x_speed;
            }

            // bounds check acceleration

            if(Math.Abs(x_acceleration_rate) > max_x_acceleration_rate)
            {
                // negative if going left, positive if going right 

                if(x_acceleration_rate < 0)
                {
                    x_acceleration_rate = (-max_x_acceleration_rate);
                }
                else
                {
                    x_acceleration_rate = max_x_acceleration_rate;
                }
            }

            return x_speed;
        }

        //====================================================================

        public float Next_X_Speed()
        {
            float new_x_speed = x_speed;
            new_x_speed += x_acceleration_rate;

            // bounds check speed
            /*
            if (Math.Abs(new_x_speed) > max_x_speed)
            {
                // negative if going left, positive if going right 

                new_x_speed = (x_speed < 0) ? (-max_x_speed) : max_x_speed;
            }
             */

            return new_x_speed;
        }

        //====================================================================

        /*----------------------------------------------
         * 
         * It is as if I have some kind of deadband. 
         * The sprite keeps rocking back and forth 
         * (without changing facing) before settling. 
         * 
         * goal is to get speed and acceleration 
         * gradually to 0 
         * 
         * -------------------------------------------*/

        public void Decelerate_X_Axis()
        {
            // going left

            if(x_speed < 0)
            {
                // bounds check to make sure we don't over-decelerate and
                // start going right 

                x_acceleration_rate += default_x_acceleration_rate;

                if(x_acceleration_rate > 0)
                {
                    StopXAxisMotion();
                }
            }

            // going right

            else
            {
                x_acceleration_rate -= default_x_acceleration_rate;

                // bounds check to make sure we don't start going 
                // left again

                if(x_acceleration_rate < 0)
                {
                    StopXAxisMotion();
                }
            }
        }
    }
}
