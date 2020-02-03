using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Valkryie.GL
{
    public class Level
    {
        //======================================================

        /*---------------------------------
         * 
         * Starting Position 
         * Game Logic Position
         * 
         * ------------------------------*/

        internal GLPosition start_;
        public GLPosition Start
        {
            get => start_;
            set => start_ = value;
        }
        
        //======================================================

        /*----------------------------------
         * 
         *  Obstacles
         * 
         * -------------------------------*/

        internal List<Obstacle> obstacles_;

        public List<Obstacle> Obstacles
        {
            get => obstacles_;
            set => obstacles_ = value;
        }

        //======================================================

        internal String backgroundImage_;
        public String BackgroundImage
        {
            get => backgroundImage_;
            set => backgroundImage_ = value;
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
            Obstacles = new List<Obstacle>();

            //------ begin parsing the file

            XmlNode root = mapfile.DocumentElement;

            for(int i = 0; i < root.ChildNodes.Count; i++)
            {
                XmlNode child = root.ChildNodes[i];

                string Type = child.Name;

                switch(Type)
                {
                    //--------------------------------------------------------

                    case ("StartingPosition"):
                    {
                        Start = new GLPosition(child);
                        break;
                    }

                    //-------------------------------------------------------

                    case ("Background"):
                    {
                        BackgroundImage = child.Attributes["ImageSource"].Value.ToString();
                        break;
                    }

                    //-------------------------------------------------------

                    case ("Obstacles"):
                    {
                        for(int j = 0; j < child.ChildNodes.Count; j++)
                        {
                            XmlNode obsNode = child.ChildNodes[j];
                            Obstacles.Add(new Obstacle(obsNode));
                        }

                        break;
                    }

                    //-------------------------------------------------------
                }
            }
        }
    }
}
