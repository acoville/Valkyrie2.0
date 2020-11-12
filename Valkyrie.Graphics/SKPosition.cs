using SkiaSharp;
using System;

namespace Valkyrie.Graphics
{
    public class SKPosition
    {
        internal SKPoint point_;
        public SKPoint SKPoint
        {
            get => point_;
            set => point_ = value;
        }

        //------------------------------------------

        internal float depth_ = 0.0f;
        public float Z
        {
            get => depth_;
            set => depth_ = value;
        }

        //------------------------------------------

        public float X
        {
            get => point_.X;
            set => point_.X = value;
        }

        //-----------------------------------------

        public float Y
        {
            get => point_.Y;
            set => point_.Y = value;
        }


        //==================================================

        public SKPosition(float x, float y, float z)
        {
            point_.X = x;
            point_.Y = y;
            depth_ = z;
        }

        //====================================================

        public SKPosition(SKPoint point)
        {
            point_ = point;
        }

        public SKPosition()
        {
            point_ = new SKPoint();
        }

        //=====================================================

        public static implicit operator SKPosition(SKPoint v)
        {
            SKPosition pos = new SKPosition(v);
            return pos;
        }

        //=====================================================

        public override bool Equals(object other)
        {
            var otherPos = (SKPosition)other;

            float x2 = otherPos.X;
            float y2 = otherPos.Y;
            float z2 = otherPos.Z;

            return (X == x2 &&
                    Y == y2 &&
                    Z == z2);
        }

        //=======================================================

        public override string ToString()
        {
            string result = "SK: X:" + X + ", Y:" + Y + ", Z:" + Z;
            return result;
        }

        //=======================================================

        public void Translate(float deltaX, float deltaY, float deltaZ)
        {
            X += deltaX;
            Y += deltaY;
            Z += deltaZ;
        }

        //------------------------------------------------

        public void Translate(float deltaX, float deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }

        //==========================================================

        public void MoveTo(float newX, float newY, float newZ = 0.0f)
        {
            X = newX;
            Y = newY;
            Z = newZ;
        }

        //============================================================

        /*----------------------------------
         * 
         * Overload returning out variables 
         * for deltaX and deltaY
         * 
         * --------------------------------*/

        public void MoveTo(float newX, float newY, float deltaX, float deltaY)
        {
            deltaX = newX - X;
            X = newX;
           
            deltaY = newY - Y;
            Y = newY;
        }

        //====================================================================

        public override int GetHashCode()
        {
            int hashCode = 381260788;
            hashCode = hashCode * -1521134295 + point_.GetHashCode();
            hashCode = hashCode * -1521134295 + SKPoint.GetHashCode();
            hashCode = hashCode * -1521134295 + depth_.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
