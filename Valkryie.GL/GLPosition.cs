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

        //---------------------------------------------

        public GLPosition(float Xparam, float Yparam)
        {
            X = Xparam;
            Y = Yparam;
        }

        //--------------------------------------------

        public GLPosition(float Xparam, float Yparam, float Zparam)
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

        static public bool operator != (GLPosition lhs, GLPosition rhs)
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
            string result = "GL: " + X + ", " + Y + ", " + Z;
            return result;
        }
    }
}
