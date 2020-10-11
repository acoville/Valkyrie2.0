using System;
using System.Collections.Generic;
using System.IO;
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

        internal GLPosition origin_ = new GLPosition();
        public GLPosition Origin
        {
            get => origin_;

            set
            {
                origin_ = value;
                Recalculate_Boundaries();
            }
        }

        //======================================================

        internal void Recalculate_Boundaries()
        {
            bottom_ = origin_.Y;
            top_ = bottom_ + pxHeight_;
            left_ = origin_.X;
            right_ = left_ + pxWidth_;

            Recalculate_Center();
        }

        //=======================================================

        internal void Recalculate_Center()
        {
            float cx = origin_.X + (pxWidth_ / 2);
            float cy = origin_.Y + (pxHeight_ / 2);

            center_.X = cx;
            center_.Y = cy;
        }

        //======================================================

        internal GLPosition center_ = new GLPosition();
        public GLPosition Center
        {
            get => center_;
        }

        //======================================================

        internal float top_ = 0.0f;
        public float Top
        {
            get => top_;
        }

        //======================================================

        internal float bottom_ = 0.0f;
        public float Bottom
        {
            get => bottom_;
        }

        //=====================================================

        internal float left_ = 0.0f;
        public float Left
        {
            get => left_;
        }

        //====================================================

        internal float right_ = 0.0f;
        public float Right
        {
            get => right_;
        }

        //======================================================

        internal int tileHeight_ = 0;
        public int TileHeight
        {
            get => tileHeight_;
            
            set
            {
                tileHeight_ = value;
                PixelHeight = tileHeight_ * 64;
            }
        }

        //-----------------------------

        internal int tileWidth_ = 0;
        public int TileWidth
        {
            get => tileWidth_;

            set
            {
                tileWidth_ = value;
                PixelWidth = tileWidth_ * 64;
            }
        }

        //======================================================

        internal float pxHeight_ = 0.0f;
        public float PixelHeight
        {
            get => pxHeight_;

            set
            {
                pxHeight_ = value;
                Recalculate_Boundaries();
            }
        }

        //======================================================
        
        internal float pxWidth_ = 0.0f;
        public float PixelWidth
        {
            get => pxWidth_;

            set
            {
                pxWidth_ = value;
                Recalculate_Boundaries();
            }
        }
        
        //======================================================

        /*-----------------------------------
         * 
         * Constructors
         * 
         * --------------------------------*/

        public GLRect()
        {
            origin_ = new GLPosition();
        }

        //-----------------------------------------------------

        public GLRect(float top, float left, float right, float bottom)
        {
            top_ = top;
            left_ = left;
            right_ = right;
            bottom_ = bottom;

            origin_ = new GLPosition(left_, bottom_);

            pxHeight_ = top_ - bottom_;
            pxWidth_ = right_ - left_;

            tileHeight_ = (int) (pxHeight_ / 64.0f);
            tileWidth_ = (int) (pxWidth_ / 64.0f);

            Recalculate_Center();
        }

        //----------------------------------------------------

        public GLRect(GLPosition origin, float height, float width)
        {
            origin_ = origin;
            
            PixelHeight = height;
            PixelWidth = width;

            tileHeight_ = (int)PixelHeight / 64;
            tileWidth_ = (int)PixelWidth / 64;

            Recalculate_Boundaries();
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
            origin_ = origin;

            TileHeight = height;
            TileWidth = width;

            PixelHeight = (float)height * 64;
            PixelWidth = (float)width * 64;

            Recalculate_Boundaries();
        }

        //=========================================================

        /*---------------------------------------
         * 
         * Create a new Rectangle containing
         * both this and another rectangle
         * 
         * ------------------------------------*/

        public GLRect(GLRect rect1, GLRect rect2)
        {
            float low_x = rect1.Left < rect2.Left? rect1.Left : rect2.Left;
            float low_y = rect1.Bottom < rect2.Bottom ? rect1.Bottom : rect2.Bottom;

            float high_x = rect1.Right > rect2.Right ? rect1.Right : rect2.Right;
            float high_y = rect1.Top > rect2.Top ? rect1.Top : rect2.Top;

            left_ = low_x;
            right_ = high_x;
            bottom_ = low_y;
            top_ = high_y;

            origin_ = new GLPosition(low_x, low_y);
            pxHeight_ = high_y - low_y;
            pxWidth_ = high_x - low_x;
            
            center_ = new GLPosition(low_x, low_y);
            Recalculate_Center();
        }

        //==========================================================================

        public void Scale_Width(float scalar)
        {
            left_ *= scalar;
            right_ *= scalar;

            Recalculate_Boundaries();
        }

        //===========================================================================

        public void Scale_Height(float scalar)
        {
            top_ *= scalar;
            bottom_ *= scalar;

            Recalculate_Boundaries();
        }

        //==========================================================================

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

        //=========================================================

        /*-----------------------------------
         * 
         * Intersects another Rect
         * 
         * ---------------------------------*/

        public bool Intersects(GLRect other)
        {
            return (Y_Overlap(other) && X_Overlap(other));
        }

        //====================================================================

        public bool Y_Overlap(GLRect other)
        {
            bool result = false;

            //-- top edge of rect 1 is between top and bottom of rect2
            
            if (Top >= other.Bottom && Top <= other.Top)
            {
                result = true;
            }

            //-- bottom edge of rect1 is between top and bottom of rect2

            else if (Bottom >= other.Bottom && Bottom <= other.Top)
            {
                result = true;
            }

            return result;
        }

        //====================================================================

        public bool X_Overlap(GLRect other)
        {
            bool result = false;

            //-- top edge of rect 1 is between top and bottom of rect2

            if (Left > other.Left && Left < other.Right)
            {
                result = true;
            }

            //-- bottom edge of rect1 is between top and bottom of rect2
            
            else if (Right >= other.Left && Right <= other.Right)
            {
                result = true;
            }

            return result;
        }

        //================================================================

        /*---------------------------
         * 
         * Move To function 
         * 
         * ------------------------*/

        public void MoveTo(GLPosition target)
        {
            origin_.MoveTo(target);
            Recalculate_Boundaries();
        }

        //==============================================================

        /*---------------------------
         * 
         * Translate
         * 
         * -------------------------*/

        public void Translate(float deltaX, float deltaY, float deltaZ = 0.0f)
        {
            origin_.Translate(deltaX, deltaY, deltaZ);
            Recalculate_Boundaries();
        }

        //==============================================================

        /*----------------------------------
         * 
         * Equality, Inequality operators
         * 
         * -------------------------------*/

        public static bool operator == (GLRect lhs, GLRect rhs)
        {
            bool result = false;

            if(lhs.Origin == rhs.Origin)
            {
                if(lhs.pxHeight_ == rhs.pxHeight_)
                {
                    if(lhs.pxWidth_ == rhs.pxWidth_)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        //===========================================================

        public override bool Equals(object obj)
        {
            var other = obj as GLRect;
            return this == other;
        }

        //==========================================================

        public override int GetHashCode()
        {
            int hash = origin_.GetHashCode();

            hash += TileHeight;
            hash += TileWidth;

            return hash;
        }

        //===========================================================

        public static bool operator != (GLRect lhs, GLRect rhs)
        {
            if (lhs.Left != rhs.Left)
                return true;

            if (lhs.Right != rhs.Right)
                return true;

            if (lhs.Bottom != rhs.Bottom)
                return true;

            if (lhs.Top != rhs.Top)
                return true;

            //-- they must be equal

            return false;
        }

        //=============================================================

        public float Vertical_Distance_Below(GLRect other)
        {
            return this.Bottom - other.Top;
        }

        //=============================================================

        public float Vertical_Distance_Above(GLRect other)
        {
            return other.Bottom - this.Top;
        }

        //=============================================================

        public float Horizontal_Distance_Right(GLRect other)
        {
            return other.Left - this.Right;
        }

        //=============================================================

        public float Horizontal_Distance_Left(GLRect other)
        {
            return this.Right - other.Left;
        }

        //============================================================

        public bool Is_Above(GLRect other)
        {
            return Bottom > other.Top ? true : false;
        }

        //============================================================

        public bool Is_Below(GLRect other)
        {
            return Top < other.Bottom ? true : false;
        }

        //=============================================================

        public void Align_Center_To(GLPosition target)
        {
            float delta_x = target.X - center_.X;
            float delta_y = target.Y - center_.Y;

            Translate(delta_x, delta_y);
        }

        //=============================================================

        public void Align_Origin_To(GLPosition target)
        {
            MoveTo(target);
        }


    }
}
