using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SkiaSharp;

namespace Valkryie.GL
{
    public class GLObstacle
    {
        //=======================================================

        /*----------------------------------------
         * 
         * Property representing the 
         * object's location in the 
         * game logic layer. This is a 
         * separte position from the on-screen
         * display position.
         * 
         * -------------------------------------*/

        internal GLRect rectangle_;
        public GLRect Rectangle
        {
            get => rectangle_;
            set => rectangle_ = value;
        }

        //=======================================================

        /*----------------------------------
         * 
         * Tile Image to use
         * 
         * -------------------------------*/

        internal string imageSource_;
        public string ImageSource
        {
            get => imageSource_;
            set => imageSource_ = value;
        }

        //===================================================================
        
        public GLObstacle()
        {
            Rectangle = new GLRect();
        }

        //===================================================================

        /*------------------------------------------
         * 
         *  Constructor accepting an 
         *  XML Node argument
         *  
         *  Creates a Rectangular obstacle
         *  using h x w 
         * 
         * ---------------------------------------*/

        public GLObstacle(XmlNode node)
        {
            //--- create the GL Rectangle 

            GLPosition origin = new GLPosition();
            origin.X = float.Parse(node.Attributes["X"].Value.ToString());
            origin.X *= 64;

            origin.Y = float.Parse(node.Attributes["Y"].Value.ToString());
            origin.Y *= 64;

            origin.Z = float.Parse(node.Attributes["Z"].Value.ToString());
            origin.Z *= 64;

            float height = float.Parse(node.Attributes["Height"].Value.ToString());
            height *= 64;

            float width = float.Parse(node.Attributes["Width"].Value.ToString());
            width *= 64;

            Rectangle = new GLRect(origin, height, width);

            // get the image

            string source = node.Attributes["Image"].Value.ToString();
            ImageSource = "Valkyrie.App.Images.Tiles." + source;
        }

        //=============================================================

        /*------------------------------------------
         * 
         * Factory Pattern 
         * 
         * Creates a list of obstacles resembling
         * a 1:1 staircase
         * 
         * ----------------------------------------*/

        public static List<GLObstacle> Staircase(XmlNode node)
        {
            List<GLObstacle> result = new List<GLObstacle>();

            int X = int.Parse(node.Attributes["X"].Value.ToString());
            int Y = int.Parse(node.Attributes["Y"].Value.ToString());
            int Z = int.Parse(node.Attributes["Z"].Value.ToString());
            
            GLPosition origin = new GLPosition(X * 64, Y * 64);
            
            int height = int.Parse(node.Attributes["Height"].Value.ToString());            
            int width = height;

            string orientation = node.Attributes["Orientation"].Value.ToString();

            string source = node.Attributes["Image"].Value.ToString();
            string ImageSource = "Valkyrie.App.Images.Tiles." + source;

            for (int i = 0; i < height; i++)
            {
                GLObstacle obs = new GLObstacle();
                obs.ImageSource = ImageSource;

                obs.Rectangle.TileHeight = 1;
                obs.Rectangle.TileWidth = width;
                width -= 1;

                obs.Rectangle.MoveTo(origin);
                result.Add(obs);

                // left aligned staircase

                if (orientation == "Left")
                {
                    origin.Translate(64.0f, 64.0f, Z);
                }

                // right aligned staircase

                else
                {
                    origin.Translate(0, 64.0f, Z);
                }
            }

            return result;
        }

        //===============================================================

        /*--------------------------------
         * 
         * MoveTo
         * 
         * ------------------------------*/

        public void MoveTo(GLPosition target)
        {
            Rectangle.MoveTo(target);
        }

        //===============================================================

        public void Translate(float deltaX, float deltaY, float deltaZ = 0.0f)
        {
            Rectangle.Translate(deltaX, deltaY, deltaZ);
        }
    }
}
