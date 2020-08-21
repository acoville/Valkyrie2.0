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
        //==========================================================================

        /*-----------------------------------------------------
         * 
         *  X Axis Characteristics
         * 
         * --------------------------------------------------*/

        //-- character's current speed in the x axis, either left or right as 
        //-- governed by the facing property

        internal float x_speed = 0.0f;

        //-- at max_x_speed, x_acceleration_rate will stop accelerating and become 0

        internal float max_x_speed = 100.0f;

        //-- max x acceleration rate 

        internal float max_x_acceleration_rate = 5.5f;

        //-- x acceleration rate, governs how much delta-X we see in each given frame
        //-- this is the value that is updated by GPVM during EvaluateHorizontalMotion()
        //-- the acceleration is dimensionless, positive or negative does not indicate
        //-- direction.

        internal float x_acceleration_rate = 0.0f;

        public float X_Acceleration_Rate
        {
            get => x_acceleration_rate;

            set
            {
                if(value <= max_x_acceleration_rate)
                {
                    x_acceleration_rate = value;
                }
                else
                {
                    x_acceleration_rate = max_x_acceleration_rate;
                }
            }
        }

        //====================================================================

        public void Accelerate()
        {
            Accelerate_X();
            Accelerate_Y();
        }

        //-----------------------------------------------

        public void Accelerate_X()
        {
            if(x_speed + x_acceleration_rate <= max_x_speed)
            {
                x_speed += x_acceleration_rate;
                x_acceleration_rate = 0.0f;
            }

            else
            {
                x_speed = max_x_speed;
                x_acceleration_rate = 0.0f;
            }
        }

        //-----------------------------------------------

        public void Accelerate_Y()
        {

        }
    }
}
