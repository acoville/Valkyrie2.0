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
        }

        //===================================================================

        /*------------------------------------------
         * 
         *  Constructor accepting an 
         *  XML Node argument
         *  
         *  I like it this way, it means I only
         *  have to change things here if I want
         *  to alter how the map stores data
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

            float height = float.Parse(node.Attributes["Height"].Value.ToString());
            height *= 64;

            float width = float.Parse(node.Attributes["Width"].Value.ToString());
            width *= 64;

            Rectangle = new GLRect(origin, height, width);

            // get the image

            string source = node.Attributes["Image"].Value.ToString();
            ImageSource = "Valkyrie.App.Images.Tiles." + source + ".tile.png";
        }
    }
}
