using System.Xml;
using Valkryie.GL;
using Valkyrie.GL;
using Valkyrie.Graphics;
using Valkyrie.Controls;

namespace Valkyrie.App.Model
{
    public class Actor
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
            set => status_ = value;
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
            //set => character_.GLPosition.MoveTo(value);
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
            //set => sprite_.Move(value);
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
        }

        //==================================================================

        public void Translate(float deltaX, float deltaY, float deltaZ = 0.0f)
        {
            character_.GLPosition.Translate(deltaX, deltaY, deltaZ);
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
                if (value <= max_x_acceleration_rate)
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
            float delta_x = Accelerate_X();
            float delta_y = Accelerate_Y();

            Translate(delta_x, delta_y);
        }

        //===================================================================

        public float Accelerate_X()
        {
            if (x_speed + x_acceleration_rate <= max_x_speed)
            {
                x_speed += x_acceleration_rate;
            }

            else
            {
                x_speed = max_x_speed;
                x_acceleration_rate = 0.0f;
            }

            return x_speed;
        }

        //===================================================================

        public float Accelerate_Y()
        {
            return 0.0f;
        }
    }
}