/*=====================================================
 * 
 * Model.Actor
 * Kinematics
 * Y Axis logic
 * 
 * ==================================================*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.App.Model
{
    public partial class Actor
    {
        internal int max_jumps_ = 1;
        internal int current_jumps_ = 0;
        public int AvailableJumps
        {
            get
            {
                return max_jumps_ - current_jumps_;
            }
        }

        //-----------------------------------------------------

        internal float y_speed_ = 0.0f;
        internal float max_y_speed_ = 175.0f;
        public float Y_Speed
        {
            get => y_speed_;
            set
            {
                y_speed_ = value;

                if((float)Math.Abs(value) > max_y_speed_)
                {
                    // going down 

                    if(value < 0)
                    {
                        y_speed_ = (-max_y_speed_);
                    }

                    // going up

                    else
                    {
                        y_speed_ = max_y_speed_;
                    }
                }
                /*
                 */
            }
        }

        //------------------------------------------------------

        internal float y_acceleration_rate_ = 0.0f;
        internal float max_y_acceleration_rate_ = 7.5f;
        internal float default_y_acceleration_rate = 7.5f;
        public float Y_Acceleration_Rate
        {
            get => y_acceleration_rate_;
            set
            {
                y_acceleration_rate_ = value;
                
                if((float)Math.Abs(value) > max_y_acceleration_rate_)
                {
                    // accelerating down 

                    if(value < 0)
                    {
                        y_acceleration_rate_ = (-max_y_acceleration_rate_);
                    }

                    // accelerating up

                    else
                    {
                        y_acceleration_rate_ = max_y_acceleration_rate_;
                    }
                }
            }
        }

        //==============================================================

        public void Jump()
        {
            current_jumps_++;
            Y_Acceleration_Rate += max_x_speed;
            standing_ = false;
        }

        //==============================================================
        
        /*
         */
        public float Accelerate_Y()
        {
            Y_Speed += y_acceleration_rate_;
            return Y_Speed;
        }

        //==============================================================

        public void Stop_Y_Axis_Motion()
        {
            y_speed_ = 0.0f;
            y_acceleration_rate_ = 0.0f;
        }

    }
}
