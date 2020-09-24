using SkiaSharp;
using System;
using System.Xml;
using Valkryie.GL;
using Valkyrie.Graphics;

namespace Valkyrie.App.Model
{
    public class Obstacle : Entity
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

            Rectangle = obstacle_.Rectangle;
        }

        //--------------------------------------------

        public Obstacle(XmlNode node)
        {
            string source = node.Attributes["Image"].Value.ToString();
            ImageSource = "Valkyrie.App.Images.Tiles." + source;

            obstacle_ = new GLObstacle(node);
            TilesGroup = new TileGroup(obstacle_);

            Rectangle = obstacle_.Rectangle;
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
    }
}
