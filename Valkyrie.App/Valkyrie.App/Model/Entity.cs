using System;
using System.Collections.Generic;
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

        public bool Is_Above(ICollidable other)
        {
            return Rectangle.Is_Above(other.Rectangle);
        }

        //------------------------------------------------

        public bool Is_Below(ICollidable other)
        {
            return Rectangle.Is_Below(other.Rectangle);
        }

        //------------------------------------------------

        public bool Is_Left_Of(ICollidable other)
        {
            var otherRect = other.Rectangle;
            return Rectangle.Right < otherRect.Left ? true : false;
        }

        //------------------------------------------------

        public bool Is_Right_Of(ICollidable other)
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
    }
}
