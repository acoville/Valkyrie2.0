using System.Xml;
using Valkryie.GL;
using Valkyrie.GL;
using Valkyrie.Graphics;
using Valkyrie.Controls;

namespace Valkyrie.App.Model
{
    public partial class Actor : ICollidable
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

            set
            {

                if(controlStatus_ != value)
                {
                    controlStatus_ = value;

                    // what direction are we facing now? 
                    // determine if we have to change facing and 
                    // mirror the Sprite 

                    if(value.DirectionalStatus.L)
                    {
                        Facing = facing.left;
                    }
                
                    //-- this did not end up being true.

                    else if(value.DirectionalStatus.R)
                    {
                        Facing = facing.right;
                    }
                }
            } 
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

        //======================================================================
        
        public GLRect Rectangle
        {
            get => GLCharacter.GLRect;
        }

        //======================================================================

        public bool Intersects(ICollidable other)
        {
            var rect1 = this.GLCharacter.GLRect;
            var rect2 = other.Rectangle;

            return rect1.Intersects(rect2);
        }

        //======================================================================

        public bool Contains(ICollidable other)
        {
            var rect1 = this.GLCharacter.GLRect;
            var rect2 = other.Rectangle;

            return rect1.Contains(rect2);
        }


        //=============================================================

        internal bool obstructed_left_ = false;
        public bool ObstructedLeft
        {
            get => obstructed_left_;

            set
            {
                obstructed_left_ = value;

                if(obstructed_left_ && x_speed < 0)
                {
                    StopXAxisMotion();
                }
            }
        }
    }
}