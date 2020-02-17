using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Valkryie.GL
{
    public class Level
    {
        internal GLRect boundaries_;
        public GLRect Boundaries
        {
            get => boundaries_;
        }

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

        internal List<GLObstacle> obstacles_;

        public List<GLObstacle> Obstacles
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
            boundaries_ = new GLRect();
            boundaries_.Origin = new GLPosition(0.0f, 0.0f);
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
            Obstacles = new List<GLObstacle>();

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

                            string type = obsNode.Attributes["Type"].Value.ToString();

                            //------------------------------------------
                            
                            if(type == "Rectangle")
                            {
                                Obstacles.Add(new GLObstacle(obsNode));
                            }

                            //------------------------------------------

                            else if(type == "Stair")
                            {
                                List<GLObstacle> staircase = GLObstacle.Staircase(obsNode);

                                foreach(var obstacle in staircase)
                                {
                                    Obstacles.Add(obstacle);
                                }
                            }
                        }

                        break;
                    }

                    //-------------------------------------------------------
                }
            }
        }
    }
}
