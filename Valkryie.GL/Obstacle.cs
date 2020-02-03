using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SkiaSharp;

namespace Valkryie.GL
{
    public class Obstacle
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

        internal SKRect rectangle_;
        public SKRect Rectangle
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
        
        public Obstacle()
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

        public Obstacle(XmlNode node)
        {
            // initialize the rectangle

            float left = float.Parse(node.Attributes["Top"].Value.ToString());
            float top = float.Parse(node.Attributes["Left"].Value.ToString());
            float right = float.Parse(node.Attributes["Right"].Value.ToString());
            float bottom = float.Parse(node.Attributes["Bottom"].Value.ToString());



            Rectangle = new SKRect(left, top, right, bottom);

            // get the image

            string source = node.Attributes["Image"].Value.ToString();
            ImageSource = "Valkyrie.App.Images.Tiles." + source;
        }
    }
}
