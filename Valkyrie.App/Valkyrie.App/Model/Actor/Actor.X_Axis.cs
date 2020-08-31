/*-------------------------------
* 
*  X Axis Characteristics
* 
* -------------------------------*/

using System;

namespace Valkyrie.App.Model
{
    public partial class Actor
    {
        internal float x_speed = 0.0f;
        internal float max_x_speed = 15.0f;

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
 
        internal float max_x_acceleration_rate = 5.5f;
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
            X_Acceleration_Rate = 0.0f;
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

        public void Decelerate_X_Axis()
        {
            if (x_speed != 0 || x_acceleration_rate != 0)
            {
                // speed is negative (moving left)

                if (x_speed < 0)
                {
                    //-- need to accelerate until x_speed = 0

                    if (Math.Abs(x_speed) < default_x_acceleration_rate)
                    {
                        StopXAxisMotion();
                    }

                    else
                    {
                        X_Acceleration_Rate += default_x_acceleration_rate;
                    }
                }

                // speed is positive (moving right)

                else
                {
                    if (x_speed < default_x_acceleration_rate)
                    {
                        StopXAxisMotion();
                    }

                    else
                    {
                        X_Acceleration_Rate -= default_x_acceleration_rate;
                    }
                }
            }
        }
    }
}
