using System.Xml;
using Valkryie.GL;
using Valkyrie.GL;
using Valkyrie.Graphics;
using Valkyrie.Controls;
using System;

namespace Valkyrie.App.Model
{
    public partial class Actor : Entity
    {
        //=====================================================================

        /*---------------------------------------
         * 
         * Control Status
         * 
         * ------------------------------------*/

        internal ControlStatus controlStatus_ = new ControlStatus();
        public ControlStatus ControlStatus
        {
            get => controlStatus_;
            set => controlStatus_ = value;
        }

        //=====================================================================

        public void TurnLeft()
        {
            if(ControlStatus.DirectionalStatus.R)
            {
                ControlStatus.DirectionalStatus.R = false;
            }

            ControlStatus.DirectionalStatus.L = true;
            Facing = facing.left;
            stationary_x_ = false;
        }

        //=====================================================================

        public void TurnRight()
        {
            if(ControlStatus.DirectionalStatus.L)
            {
                ControlStatus.DirectionalStatus.L = false;
            }

            ControlStatus.DirectionalStatus.R = true;
            Facing = facing.right;
            stationary_x_ = false;
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
         *  Cache related state 
         * 
         * ----------------------------------*/

        public bool Stationary
        {
            get
            {
                return stationary_x_ && stationary_y_;
            }
        }

        internal bool stationary_x_ = true;
        internal bool stationary_y_ = true;

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

            Rectangle = GLCharacter.GLRect;
        }

        //==================================================================

        public Actor(XmlNode node)
        {
            GLCharacter = new GLCharacter(node);
            imageSource_ = GLCharacter.SpriteSource;
            Sprite = new Sprite();
            ControlStatus = new ControlStatus();

            Rectangle = GLCharacter.GLRect;
        }

        //====================================================================

        public void Accelerate()
        {
            float delta_x = Accelerate_X();
            float delta_y = Accelerate_Y();

            Translate(delta_x, delta_y);
        }
        
        //=====================================================================

        /*------------------------------------------
         * 
         *  Once the deltas have been calculated
         *  using .Accelerate(), Translate() can 
         *  be invoked to actually move the Actor.
         * 
         * ----------------------------------------*/

        public void Translate(float deltaX, float deltaY, float deltaZ = 0.0f)
        {
            //-- the Game Logic position translates with normal deltaY
            // where the Y origin is at the bottom of the screen.

            character_.Translate(deltaX, deltaY, deltaZ);

            //-- the SKPosition has to invert deltaY owing to the Y origin 
            // being at the top of the screen in Skia

            Sprite.Translate(deltaX, (-deltaY), deltaZ);
        }

        //======================================================================

        /*------------------------------------
         * 
         * MoveTo function 
         * 
         * accepts a Game Logic position as
         * argument
         * 
         * ----------------------------------*/

        public void MoveTo(GLPosition target)
        {
            float deltaX = target.X - GLPosition.X;
            float deltaY = target.Y - GLPosition.Y;
            float deltaZ = target.Z - GLPosition.Z;

            Translate(deltaX, deltaY, deltaZ);
        }

        //=============================================================

        internal bool obstructed_left_ = false;
        public bool ObstructedLeft
        {
            get => obstructed_left_;

            set
            {
                obstructed_left_ = value;

                if(obstructed_left_ && x_speed < 0 
                    || obstructed_left_ && x_acceleration_rate < 0)
                {
                    StopXAxisMotion();
                }
            }
        }

        //===========================================================

        internal bool obstructed_right_ = false;
        public bool ObstructedRight
        {
            get => obstructed_right_;

            set
            {
                obstructed_right_ = value;

                if (obstructed_right_ && x_speed > 0
                    || obstructed_right_ && x_acceleration_rate > 0)
                {
                    StopXAxisMotion();
                }
            }
        }

        //=================================================================

        /*-----------------------------------------
         * 
         * Uncertainty Region based 
         * collision detection method
         * 
         * ---------------------------------------*/

        public bool Intersects_Uncertainty_Region(ICollidable other, 
                                                    int frames = 1, 
                                                    float x_scalar = 0.5f, 
                                                    float y_scalar = 0.5f)
        {
            //-- determine where this Actor is going to be x frames from now

            GLRect nextRect = new GLRect(Rectangle.Top, Rectangle.Left, Rectangle.Right, Rectangle.Bottom);
            
            float deltaX = Next_X_Speed();
            deltaX *= frames;

            float deltaY = Next_Y_Speed();
            deltaY *= frames;

            nextRect.Translate(deltaX, deltaY);

            //-----------------------------------------------------------

            GLRect uncertainty_region = new GLRect(this.Rectangle, nextRect);

            if(x_scalar != 1.0f)
            {
                uncertainty_region.Scale_Width(x_scalar);
            }
            
            if(y_scalar != 1.0f)
            {
                uncertainty_region.Scale_Height(y_scalar);
            }

            return uncertainty_region.Intersects(other.Rectangle);
        }


        //=================================================================

        /*---------------------------------------
         * 
         * Attempts to predict where the actor
         * will be in the next iteration and 
         * weather that position will intersect
         * 
         * ------------------------------------*/

        public bool Is_About_To_Intersect_X(ICollidable other, int frames = 1)
        {
            GLRect nextRect = new GLRect(Rectangle.Top, Rectangle.Left, Rectangle.Right, Rectangle.Bottom);

            float deltaX = Next_X_Speed();

            deltaX *= frames;

            nextRect.Translate(deltaX, 0.0f);

            return nextRect.Intersects(other.Rectangle);
        }

        //=================================================================

        public bool Is_About_To_Intersect_Y(ICollidable other, int frames = 1)
        {
            GLRect nextRect = new GLRect(Rectangle.Top, Rectangle.Left, Rectangle.Right, Rectangle.Bottom);
            float deltaY = Next_Y_Speed();
            deltaY *= frames;
            nextRect.Translate(0.0f, deltaY);
            return nextRect.Intersects(other.Rectangle);
        }

        //=================================================================

        internal bool standing_ = true;
        public bool Standing
        {
            get => standing_;
            set => standing_ = value;
        }
    }
}