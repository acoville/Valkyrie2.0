using System.Xml;
using Valkryie.GL;
using Valkyrie.GL;
using Valkyrie.Graphics;
using Valkyrie.Controls;
using Windows.UI.Input;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;

namespace Valkyrie.App.Model
{
    public class Actor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
                RaisePropertyChanged();
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
            //float delta_y = Accelerate_Y();

            float delta_y = 0.0f;

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

        //---------------------------------------------------------------------
        // SPEED

        //-- character's current speed in the x axis, either left or right as 
        //-- governed by the facing property

        internal float x_speed = 0.0f;

        //-- at max_x_speed, x_acceleration_rate will stop accelerating and become 0

        internal float max_x_speed = 50.0f;

        //---------------------------------------------------------------------
        // ACCELERATION

        //-- default acceleration rate, which might be changed by powerups, etc 
        //-- so it should have a public property

        internal float default_x_acceleration_rate = 10.5f;
        public float DefaultXAccelRate
        {
            get => default_x_acceleration_rate;
            set => default_x_acceleration_rate = value;
        }

        //-- max x acceleration rate 

        internal float max_x_acceleration_rate = 5.5f;

        //-- x acceleration rate, governs how much delta-X we see in each given frame
        //-- this is the value that is updated by GPVM during EvaluateHorizontalMotion()

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

                if(absValue > max_x_acceleration_rate)
                {
                    if(value < 0)
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

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}