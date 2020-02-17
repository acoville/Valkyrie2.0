using System.Xml;

namespace Valkryie.GL
{
    public class GLPosition
    {
        //================================================

        internal float x_;
        public float X
        {
            get => x_;
            set => x_ = value;
        }

        //================================================

        internal float y_;
        public float Y
        {
            get => y_;
            set => y_ = value;
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

        //=================================================================

        static public bool operator == (GLPosition lhs, GLPosition rhs)
        {
            return (lhs.X == rhs.X && lhs.Y == rhs.Y);
        }

        //================================================================

        static public bool operator != (GLPosition lhs, GLPosition rhs)
        {
            return (lhs.X != rhs.X || lhs.Y != rhs.Y);
        }

        //===============================================================

        public override int GetHashCode()
        {
            return (int)X + (int)Y;
        }

        //===============================================================

        public override bool Equals(object obj)
        {
            var other = obj as GLPosition;

            return (other == this);
        }
    }
}
