using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Valkryie.GL
{
    public class Level
    {
        internal String imageSource_;
        public String ImageSource
        {
            get => imageSource_;
            set
            {
                imageSource_ = value;
            }
        }

        //======================================================

        /*-----------------------------------
         * 
         * 
         * ---------------------------------*/

        public Level()
        {

        }

        //======================================================

        /*-----------------------------------------
         * 
         * Constructor accepting an 
         * XML Document, parses all nodes 
         * in no particular order and populates
         * level data with it
         * 
         * ---------------------------------------*/

        public Level(XmlDocument mapfile)
        {
            XmlNode root = mapfile.DocumentElement;

            for(int i = 0; i < root.ChildNodes.Count; i++)
            {
                XmlNode child = root.ChildNodes[i];

                string Type = child.Name;

                switch(Type)
                {
                    //-------------------------------------------------------

                    case ("Background"):
                    {
                        ImageSource = child.Attributes["ImageSource"].Value.ToString();
                        break;
                    }

                    //-------------------------------------------------------
                }
            }
        }
    }
}
