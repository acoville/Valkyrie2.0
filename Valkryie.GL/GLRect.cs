﻿using System;
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

        internal int tileHeight_;
        public int TileHeight
        {
            get => tileHeight_;
            set => tileHeight_ = value;
        }

        //-----------------------------

        internal int tileWidth_;
        public int TileWidth
        {
            get => tileWidth_;
            set => tileWidth_ = value;
        }

        //======================================================

        internal float pxHeight_;
        public float PixelHeight
        {
            get => pxHeight_;
            set => pxHeight_ = value;
        }

        //======================================================
        
        internal float pxWidth_;
        public float PixelWidth
        {
            get => pxWidth_;
            set => pxWidth_ = value;
        }
        
        //======================================================

        /*-----------------------------------
         * 
         * Constructors
         * 
         * --------------------------------*/

        public GLRect()
        {}

        //-----------------------------------------------------

        public GLRect(float top, float left, float right, float bottom)
        {
            top_ = top;
            left_ = left;
            right_ = right;
            bottom_ = bottom;

            Origin = new GLPosition(left_, bottom_);

            float center_x = Origin.X + (right / 2.0f);
            float center_y = Origin.Y + (top / 2.0f);

            center_ = new GLPosition(center_x, center_y);
        }

        //----------------------------------------------------

        public GLRect(GLPosition origin, float height, float width)
        {
            Origin = origin;
            PixelHeight = height;
            PixelWidth = width;

            TileHeight = (int)PixelHeight / 64;
            TileWidth = (int)PixelWidth / 64;

            Top = Origin.Y + PixelHeight;
            Bottom = Origin.Y;
            Left = Origin.X;
            Right = Origin.X + PixelWidth;

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

            TileHeight = height;
            TileWidth = width;

            PixelHeight = (float)height * 64;
            PixelWidth = (float)width * 64;

            Top = Origin.Y + PixelHeight;
            Bottom = Origin.Y;
            Left = Origin.X;
            Right = Origin.X + PixelWidth;

            float center_x = Origin.X + (PixelWidth / 2.0f);
            float center_y = Origin.Y + (PixelHeight / 2.0f);

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

            if(position.X >= Origin.X && position.X <= Origin.X + PixelWidth)
            {
                withinX = true;
            }

            if(position.Y >= Origin.Y && position.Y <= Origin.Y + PixelHeight)
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
