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
        public float Depth
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
            float z2 = otherPos.Depth;

            return (X == x2 &&
                    Y == y2 &&
                    Depth == z2);
        }

        //=======================================================

        public override string ToString()
        {
            string result = "SK: " + X + ", " + Y + ", " + Depth;
            return result;
        }
    }
}
