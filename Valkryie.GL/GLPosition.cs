using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Valkryie.GL
{
    public class GLPosition
    {
        //-------------------------------------------------

        internal float x_ = 0.0f;
        public float X
        {
            get => x_;
            set => x_ = value;
        }

        //-------------------------------------------------

        internal float y_ = 0.0f;
        public float Y
        {
            get => y_;
            set => y_ = value;
        }

        //-------------------------------------------------

        internal float z_ = 1.0f;
        public float Z
        {
            get => z_;
            set => z_ = value;
        }

        //================================================

        /*------------------------------
         * 
         * Constructors
         * 
         * ---------------------------*/

        public GLPosition()
        {}

        //--------------------------------------------

        public GLPosition(float Xparam, float Yparam, float Zparam = 1.0f)
        {
            X = Xparam;
            Y = Yparam;
            Z = Zparam;
        }

        //====================================================================

        /*---------------------------------------
         * 
         * Constructor called by the Level 
         * class, positions in a map.xml file
         * will be defined in block coordinates, 
         * so for now we are hand-coding the 
         * multiplicand as 64x64 pixel block 
         * sizes.
         * 
         * ------------------------------------*/

        public GLPosition(XmlNode node)
        {
            X = float.Parse(node.Attributes["X"].Value.ToString());
            X *= 64.0f;

            Y = float.Parse(node.Attributes["Y"].Value.ToString());
            Y *= 64.0f;

            Z = float.Parse(node.Attributes["Z"].Value.ToString());
            Z *= 64.0f;
        }

        //=================================================================

        /*------------------------------
         * 
         * Move To 
         * 
         * ----------------------------*/

        public void MoveTo(GLPosition target)
        {
            X = target.X;
            Y = target.Y;
            Z = target.Z;
        }

        //===============================================================

        /*--------------------------------
         * 
         * Translate
         * 
         * ------------------------------*/

        public void Translate(float deltaX, float deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }

        //------------------------------------------------

        public void Translate(float deltaX, float deltaY, float deltaZ)
        {
            X += deltaX;
            Y += deltaY;
            Z += deltaZ;
        }

        //=================================================================

        static public bool operator == (GLPosition lhs, GLPosition rhs)
        {
            return (lhs.X == rhs.X 
                        && lhs.Y == rhs.Y 
                        && lhs.Z == rhs.Z);
        }

        //================================================================

        static public bool operator !=(GLPosition lhs, GLPosition rhs)
        {
            return (lhs.X != rhs.X 
                        || lhs.Y != rhs.Y
                        || lhs.Z != rhs.Z);
        }

        //===============================================================

        public override int GetHashCode()
        {
            return (int)X + (int)Y + (int)Z;
        }

        //===============================================================

        public override bool Equals(object obj)
        {
            var other = obj as GLPosition;
            return (other == this);
        }

        //==============================================================

        public override string ToString()
        {
            string result = "GL: X:" + X + ", Y:" + Y + ", Z:" + Z;
            return result;
        }

        //==============================================================

        /*-----------------------------------
         * 
         * Helper Function to return
         * the distance between 2 
         * GL Positions
         * 
         * --------------------------------*/

        public float DistanceTo(GLPosition target)
        {
            var deltaX = Horizontal_Distance_To(target);
            var deltaY = Vertical_Distance_To(target);
            float distance;

            // quick check to see if we can avoid pytharogrean
            // which is going to be computationally expensive

            if(deltaX == 0)
            {
                distance = deltaY == 0 ? 0 : deltaY;
                goto END;
            }

            else if(deltaY == 0)
            {
                distance = deltaX;
                goto END;
            }

            // otherwise we need to use Pythagorean for a 2D solution
            // this is not accounting for the Z coordinate plane
            
            else
            {
            }

            END:

            var A_squared = Math.Pow(deltaX, 2);
            var B_squared = Math.Pow(deltaY, 2);

            distance = (float)Math.Sqrt(A_squared + B_squared);
            return distance;
        }

        //==============================================================

        public float Horizontal_Distance_To(GLPosition target)
        {
            var x1 = this.X;
            var x2 = target.X;

            return Math.Abs(x2 - x1);
        }

        //=============================================================

        public float Vertical_Distance_To(GLPosition target)
        {
            var y1 = this.Y;
            var y2 = target.Y;

            return Math.Abs(y2 - y1);
        }
    }
}
