using SkiaSharp;
using System;
using System.Xml;
using Valkryie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class Obstacle : ICollidable
    {
        internal string imageSource_;
        public string ImageSource
        {
            get => imageSource_;
            set => imageSource_ = value;
        }

        //==================================================

        public GLPosition GLPosition
        {
            get => GLObs.Rectangle.Origin;
            set => GLObs.MoveTo(value);
        }

        //=================================================

        public SKPosition SKPosition
        {
            get => tilegroup_.SKPosition;
            set => tilegroup_.Move(value);
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
         * Constructors
         * 
         * ---------------------------------*/

        public Obstacle(GLObstacle globs)
        {
            obstacle_ = globs;
            TilesGroup = new TileGroup(obstacle_);
        }

        //--------------------------------------------

        public Obstacle(XmlNode node)
        {
            string source = node.Attributes["Image"].Value.ToString();
            ImageSource = "Valkyrie.App.Images.Tiles." + source;

            obstacle_ = new GLObstacle(node);
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
         * Translate (game logic coordinate)
         * 
         * ------------------------------*/

        public void Translate(float deltaX, float deltaY, float deltaZ = 0.0f)
        {
            GLObs.Translate(deltaX, deltaY, deltaZ);
            tilegroup_.Translate(deltaX, (-deltaY), deltaZ);
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

        public float Vertical_Distance_Above(ICollidable other)
        {
            return Rectangle.Vertical_Distance_Above(other.Rectangle);
        }

        //====================================================================

        public float Vertical_Distance_Below(ICollidable other)
        {
            return Rectangle.Vertical_Distance_Below(other.Rectangle);
        }

        //====================================================================

        public bool Contains(ICollidable other)
        {
            var rect1 = this.GLObs.Rectangle;
            var rect2 = other.Rectangle;

            return rect1.Contains(rect2);
        }

        //=====================================================================

        public bool Is_Above(ICollidable other)
        {
            return GLObs.Rectangle.Is_Above(other.Rectangle);
        }

        //=====================================================================

        public bool Is_Below(ICollidable other)
        {
            return GLObs.Rectangle.Is_Below(other.Rectangle);
        }

        //===================================================================

        public bool Is_Left_Of(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return Rectangle.Right < otherRect.Left ? true : false;
        }

        //===================================================================

        public bool Is_Right_Of(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return Rectangle.Left > otherRect.Right ? true : false;
        }

        //===================================================================

        public GLRect Rectangle
        {
            get => GLObs.Rectangle;
        }
        
    }
}
