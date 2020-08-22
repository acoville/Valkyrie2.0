using System.Xml;
using Valkryie.GL;
using Valkyrie.GL;
using Valkyrie.Graphics;
using Valkyrie.Controls;
using Windows.UI.Input;
using System.ComponentModel;

namespace Valkyrie.App.Model
{
    public class Actor : INotifyPropertyChanged
    {
        //=====================================================================

        /*---------------------------------------
         * 
         * Control Status
         * 
         * ------------------------------------*/

        internal ControlStatus status_;
        public ControlStatus ControlStatus
        {
            get => status_;

            set
            {
                // what direction are we facing now? 
                // determine if we have to change facing and 
                // mirror the Sprite 

                var current_facing = Facing;

                if(value.DirectionalStatus.L && (current_facing == facing.right))
                {
                    facing_ = facing.left;
                    Sprite.Mirror();
                }
                
                else if(value.DirectionalStatus.R && (current_facing == facing.left))
                {
                    facing_ = facing.right;
                    Sprite.Mirror();
                }

                status_ = value;
            } 
        }

        //=====================================================================

        public enum facing
        {
            left, right
        };

        internal facing facing_ = facing.right;
        public facing Facing
        {
            get => facing_;
            
            set
            {
                var oldFacing = facing_;
                
                facing_ = value;

                if(facing_ != oldFacing)
                {
                    sprite_.Mirror();
                }
            }
        }

        //=====================================================================

        /*------------------------------------
         * 
         * Valkyrie.Game Logic related state
         * 
         * ----------------------------------*/

        internal GLCharacter character_;
        public GLCharacter GLCharacter
        {
            get => character_;
            set => character_ = value;
        }

        //--------------------------------------------------

        public GLPosition GLPosition
        {
            get => character_.GLPosition;
        }

        //-------------------------------------------------

        public int Team
        {
            get => character_.Team;
            set => character_.Team = value;
        }

        //====================================================================

        /*-----------------------------------
         * 
         * Valkyrie.Graphics related state
         * 
         * --------------------------------*/

        internal Sprite sprite_;
        public Sprite Sprite
        {
            get => sprite_;
            set => sprite_ = value;
        }

        //-------------------------------------------------

        public SKPosition SKPosition
        {
            get => sprite_.SKPosition;
        }

        //--------------------------------------------------

        internal string imageSource_;
        public string ImageSource
        {
            get => imageSource_;
            set => imageSource_ = value;
        }

        //===================================================================

        /*--------------------------------
         * 
         * Valkyrie.Model 
         * 
         * -----------------------------*/

        public Actor(GLCharacter character)
        {
            GLCharacter = character;
            imageSource_ = GLCharacter.SpriteSource;
            Sprite = new Sprite();
        }

        //==================================================================

        public Actor(XmlNode node)
        {
            GLCharacter = new GLCharacter(node);
            imageSource_ = GLCharacter.SpriteSource;
            Sprite = new Sprite();
            ControlStatus = new ControlStatus();
        }

        //====================================================================

        public void Accelerate()
        {
            float delta_x = Accelerate_X();
            float delta_y = Accelerate_Y();

            Translate(delta_x, delta_y);
        }

        //==================================================================

        /*------------------------------------------
         * 
         *  Once the deltas have been calculated
         *  using .Accelerate(), Translate() can 
         *  be invoked to actually move the Actor.
         * 
         * ----------------------------------------*/

        internal void Translate(float deltaX, float deltaY, float deltaZ = 0.0f)
        {
            if(Facing == facing.left)
            {
                deltaX = (-deltaX);
            }

            //-- the Game Logic position translates with normal deltaY
            // where the Y origin is at the bottom of the screen.

            character_.GLPosition.Translate(deltaX, deltaY, deltaZ);

            //-- the SKPosition has to invert deltaY owing to the Y origin 
            // being at the top of the screen in Skia

            Sprite.Translate(deltaX, (-deltaY), deltaZ);
        }

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

        internal float max_x_speed = 20.0f;

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
                if (value < 0)
                {
                    x_acceleration_rate = 0.0f;
                }

                else if (value <= max_x_acceleration_rate)
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
            //-- if we are below max speed, increment speed  

            if (x_speed + x_acceleration_rate <= max_x_speed)
            {
                x_speed += x_acceleration_rate;
            }

            //-- if we have gotten to max speed, then stop accelerating

            else
            {
                x_speed = max_x_speed;
                x_acceleration_rate = 0.0f;
            }

            return x_speed;
        }

        //==========================================================================

        /*-----------------------------------------------------
         * 
         *  Y Axis Characteristics
         * 
         * --------------------------------------------------*/

        //-- character's current speed in the y axis, either left or right as 
        //-- governed by the facing property

        internal float y_speed = 0.0f;

        //-- at max_y_speed, y_acceleration_rate will stop accelerating and become 0

        internal float max_y_speed = 100.0f;

        //-- max y acceleration rate 

        internal float max_y_acceleration_rate = 5.5f;

        //-- y acceleration rate, governs how much delta-Y we see in each given frame
        //-- this is the value that is updated by GPVM during EvaluateVerticalMotion()

        internal float y_acceleration_rate = 0.0f;

        public event PropertyChangedEventHandler PropertyChanged;

        public float Y_Acceleration_Rate
        {
            get => y_acceleration_rate;

            set
            {
                if (value < 0)
                {
                    y_acceleration_rate = 0.0f;
                }

                else if (value <= max_y_acceleration_rate)
                {
                    y_acceleration_rate = value;
                }

                else
                {
                    y_acceleration_rate = max_y_acceleration_rate;
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

        public void StopYAxisMotion()
        {
            Y_Acceleration_Rate = 0.0f;
            y_speed = 0.0f;
        }

        //===================================================================

        public float Accelerate_Y()
        {
            if (y_speed + y_acceleration_rate <= max_y_speed)
            {
                y_speed += y_acceleration_rate;
            }

            else
            {
                y_speed = max_y_speed;
                y_acceleration_rate = 0.0f;
            }

            return y_speed;
        }
    }
}