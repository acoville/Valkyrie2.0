using SkiaSharp;
using Valkryie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class Obstacle
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

        public SKPoint SKPosition
        {
            get => tilegroup_.SkiaOrigin;
            
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

        public void MoveSprite(SKPoint target)
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
            SKPoint newpoint = SKPosition;
            newpoint.X += deltaX;
            newpoint.Y += deltaY;

            SKPosition = newpoint;
        }
    }
}
