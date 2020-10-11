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

        /*-----------------------------------------

              Uncertainty Region Property

        -----------------------------------------*/

        internal GLRect uncertainty_region_ = new GLRect();
        public GLRect Uncertainty_Region
        {
            get => uncertainty_region_;
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

            //-- I need to initialize this to at least a 1x1 block 
            // or else we get no collision detection. 

            Reset_Uncertainty_Region();
        }

        //==================================================================

        internal void Reset_Uncertainty_Region()
        {
            uncertainty_region_.Align_Origin_To(GLPosition);
            uncertainty_region_.PixelHeight = Rectangle.PixelHeight;
            uncertainty_region_.PixelWidth = Rectangle.PixelWidth;
        }

        //==================================================================

        public Actor(XmlNode node)
        {
            GLCharacter = new GLCharacter(node);
            imageSource_ = GLCharacter.SpriteSource;
            Sprite = new Sprite();
            ControlStatus = new ControlStatus();

            Rectangle = GLCharacter.GLRect;
            Reset_Uncertainty_Region();
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
            GLRect rect1 = new GLRect(Rectangle.Top, Rectangle.Left, Rectangle.Right, Rectangle.Bottom);

            //-- the Game Logic position translates with normal deltaY
            // where the Y origin is at the bottom of the screen.

            character_.Translate(deltaX, deltaY, deltaZ);

            //-- the SKPosition has to invert deltaY owing to the Y origin 
            // being at the top of the screen in Skia

            Sprite.Translate(deltaX, (-deltaY), deltaZ);

            //-- update the uncertainty region

            GLRect rect2 = new GLRect(Rectangle.Top, Rectangle.Left, Rectangle.Right, Rectangle.Bottom);
            uncertainty_region_ = new GLRect(rect1, rect2);
        }

        //======================================================================

        /*--------------------------------------------
         * 
         * The following overrides of Entity base 
         * class are meant to utilize the Actor's
         * uncertainty region instead of its present 
         * Game Logic rectangle to resolve collision
         * detection logic
         * 
         * ----------------------------------------*/

        public override bool Is_Left_Of(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return uncertainty_region_.Right < otherRect.Left ? true : false;
        }

        //-----------------------------------------

        public override bool Is_Right_Of(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return uncertainty_region_.Left > otherRect.Right ? true : false;
        }

        //------------------------------------------

        public override bool Is_Above(ICollidable other)
        {
            return uncertainty_region_.Is_Above(other.Rectangle);
        }

        //------------------------------------------

        public override bool Is_Below(ICollidable other)
        {
            return uncertainty_region_.Is_Below(other.Rectangle);
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
            Reset_Uncertainty_Region();
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

        public bool Intersects_Uncertainty_Region(ICollidable other)
        {
            return uncertainty_region_.Intersects(other.Rectangle);
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