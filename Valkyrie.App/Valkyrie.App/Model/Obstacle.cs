using SkiaSharp;
using Valkryie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class Obstacle : ICollidable
    {
        public GLPosition GLPosition
        {
            get => GLObs.Rectangle.Origin;

            set
            {
                GLObs.MoveGLRectTo(value);
            }
        }

        //=================================================

        public SKPosition SKPosition
        {
            get => tilegroup_.SKPosition;
            
            set
            {
                tilegroup_.Move(value);
            }
        }

        //=================================================

        internal GLObstacle obstacle_;
        public GLObstacle GLObs
        {
            get => obstacle_;
            set => obstacle_ = value;
        }

        //================================================

        internal TileGroup tilegroup_;
        public TileGroup TilesGroup
        {
            get => tilegroup_;
            set => tilegroup_ = value;
        }

        //================================================

        /*-----------------------------------
         * 
         * Constructor
         * 
         * ---------------------------------*/

        public Obstacle(GLObstacle globs)
        {
            obstacle_ = globs;
            TilesGroup = new TileGroup(obstacle_);
        }

        //==================================================

        /*--------------------------------
         * 
         * Move Sprite
         * 
         * ------------------------------*/

        public void MoveSprite(SKPosition target)
        {
            tilegroup_.Move(target);
        }

        //==================================================

        /*--------------------------------
         * 
         * Translate Sprite
         * 
         * ------------------------------*/

        public void TranslateSprite(float deltaX, float deltaY)
        {
            SKPoint newpoint = SKPosition.SKPoint;
            newpoint.X += deltaX;
            newpoint.Y += deltaY;

            SKPosition = newpoint;
        }

        //====================================================================

        public bool Intersects(ICollidable other)
        {
            var rect1 = this.GLObs.Rectangle;
            var rect2 = other.Rectangle;

            return rect1.Intersects(rect2);
        }

        //====================================================================

        public bool Contains(ICollidable other)
        {
            var rect1 = this.GLObs.Rectangle;
            var rect2 = other.Rectangle;

            return rect1.Contains(rect2);
        }

        //===================================================================

        public GLRect Rectangle
        {
            get
            {
                return GLObs.Rectangle;
            }
        }
        
    }
}
