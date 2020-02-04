using System;
using System.Collections.Generic;
using System.Text;

namespace Valkryie.GL
{
    public class GLRect
    {
        //========================================================

        /*---------------------------------------
         * 
         * Properties
         * 
         * ------------------------------------*/

        internal GLPosition origin_;
        public GLPosition Origin
        {
            get => origin_;
            set => origin_ = value;
        }

        //======================================================

        internal GLPosition center_;
        public GLPosition Center
        {
            get => center_;
        }

        //======================================================

        internal float top_;
        public float Top
        {
            get => top_;
            set => top_ = value;
        }

        //======================================================

        internal float bottom_;
        public float Bottom
        {
            get => bottom_;
            set => bottom_ = value;
        }

        //=====================================================

        internal float left_;
        public float Left
        {
            get => left_;
            set => left_ = value;
        }

        //====================================================

        internal float right_;
        public float Right
        {
            get => right_;
            set => right_ = value;
        }

        //======================================================

        internal float height_;
        public float Height
        {
            get => height_;
            set => height_ = value;
        }

        //======================================================
        
        internal float width_;
        public float Width
        {
            get => width_;
            set => width_ = value;
        }
        
        //======================================================

        /*-----------------------------------
         * 
         * Constructors
         * 
         * --------------------------------*/

        public GLRect(GLPosition origin, float height, float width)
        {
            Origin = origin;
            Height = height;
            Width = width;

            Top = Origin.Y + Height;
            Bottom = Origin.Y;
            Left = Origin.X;
            Right = Origin.X + Width;

            float center_x = Origin.X + (width / 2.0f);
            float center_y = Origin.Y + (height / 2.0f);

            center_ = new GLPosition(center_x, center_y);
        }

        //======================================================

        /*-------------------------------------
         * 
         * Constructor Overload accepting
         * number of tiles in place of pixels
         * 
         * ----------------------------------*/

        public GLRect(GLPosition origin, int height, int width)
        {
            Origin = origin;
            Height = (float)height * 64;
            Width = (float)width * 64;

            Top = Origin.Y + Height;
            Bottom = Origin.Y;
            Left = Origin.X;
            Right = Origin.X + Width;

            float center_x = Origin.X + (Width / 2.0f);
            float center_y = Origin.Y + (Height / 2.0f);

            center_ = new GLPosition(center_x, center_y);
        }

        //=========================================================

        /*--------------------------------------
         * 
         * Contains function indicates weather
         * a point is located inside the 
         * rectangle
         * 
         --------------------------------------*/

        public bool Contains(GLPosition position)
        {
            bool withinX = false;
            bool withinY = false;

            if(position.X >= Origin.X && position.X <= Origin.X + Width)
            {
                withinX = true;
            }

            if(position.Y >= Origin.Y && position.Y <= Origin.Y + Height)
            {
                withinY = true;
            }

            return(withinX && withinY) ? true : false;
        }

        //======================================================

        /*----------------------------------
         * 
         * Contains overload indicating
         * weather 1 GLRect contains another
         * 
         * --------------------------------*/

        public bool Contains(GLRect other)
        {
            bool XinRange = false;
            bool YinRange = false;

            if (other.Left >= this.Left && other.Right <= this.Right)
            {
                XinRange = true;
            }

            if(other.Top <= this.Top && other.Bottom >= this.Bottom)
            {
                YinRange = true;
            }

            return (XinRange && YinRange) ? true : false;
        }

        //===========================================================

        /*----------------------------------
         * 
         * Helper Function to determine 
         * weather a given Y coordinate
         * is within the Y range of this
         * rectangle
         * 
         * ------------------------------*/

        internal bool YIntersects(float arg)
        {
            return (arg < this.Top && arg > this.Bottom) ? true : false;
        }

        /*------------------------------
         * 
         * Same for X intersection
         * 
         * ----------------------------*/

        internal bool XIntersects(float arg)
        {
            return (arg > this.Left && arg < this.Right) ? true : false;
        }

        //=========================================================

        /*-----------------------------------
         * 
         * Intersects another Rect
         * 
         * ---------------------------------*/

        public bool Intersects(GLRect other)
        {
            bool YOverlap = false;
            
            if(YIntersects(other.Top) || YIntersects(other.Bottom))
            {
                YOverlap = true;
            }
            
            //-----------------------------------------
            
            bool XOverlap = false;

            if(XIntersects(other.Left) || XIntersects(other.Right))
            {
                XOverlap = true;
            }

            return (XOverlap || YOverlap) ? true : false;
        }
    }
}
