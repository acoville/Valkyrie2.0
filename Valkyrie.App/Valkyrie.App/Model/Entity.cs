using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Valkryie.GL;

namespace Valkyrie.App.Model
{
    public class Entity : ICollidable
    {
        public GLRect Rectangle
        {
            get;
            set;
        }

        //======================================================================

        /*----------------------------------
         * 
         * ICollidable interface 
         * 
         * -------------------------------*/
        
        public bool Contains(ICollidable other)
        {
            var rect1 = this.Rectangle;
            var rect2 = other.Rectangle;

            return rect1.Contains(rect2);
        }

        //------------------------------------------------

        public bool Intersects(ICollidable other)
        {
            var rect1 = this.Rectangle;
            var rect2 = other.Rectangle;

            return rect1.Intersects(rect2);
        }

        //------------------------------------------------

        public virtual bool Is_Above(ICollidable other)
        {
            return Rectangle.Is_Above(other.Rectangle);
        }

        //------------------------------------------------

        public virtual bool Is_Below(ICollidable other)
        {
            return Rectangle.Is_Below(other.Rectangle);
        }

        //------------------------------------------------

        public virtual bool Is_Left_Of(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return Rectangle.Right < otherRect.Left ? true : false;
        }

        //------------------------------------------------

        public virtual bool Is_Right_Of(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return Rectangle.Left > otherRect.Right ? true : false;
        }

        //------------------------------------------------

        public float Vertical_Distance_Above(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return Rectangle.Vertical_Distance_Above(otherRect);
        }

        //------------------------------------------------

        public float Vertical_Distance_Below(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return Rectangle.Vertical_Distance_Below(otherRect);
        }

        //------------------------------------------------

        public float Horizontal_Distance_Right(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return Rectangle.Horizontal_Distance_Right(otherRect);
        }

        //--------------------------------------------------

        public float Horizontal_Distance_Left(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return Rectangle.Horizontal_Distance_Left(otherRect);
        }

        //====================================================================

        public bool Y_Overlap(ICollidable other)
        {
            GLRect rect1 = Rectangle;
            GLRect rect2 = other.Rectangle;

            return rect1.Y_Overlap(rect2);
        }

        //===================================================================

        public bool X_Overlap(ICollidable other)
        {
            GLRect rect1 = Rectangle;
            GLRect rect2 = other.Rectangle;

            return rect1.X_Overlap(rect2);
        }

        
    }
}
