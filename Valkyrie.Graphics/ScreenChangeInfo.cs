using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.Graphics
{
    public class ScreenChangeInfo
    {
        internal double deltaX = 0.0f;
        public double DeltaX
        {
            get => deltaX;
            set => deltaX = value;
        }

        //-------------------------------------------

        internal double deltaY = 0.0f;
        public double DeltaY
        {
            get => deltaY;
            set => deltaY = value;
        }

        //===========================================================

        public ScreenChangeInfo(double delta_X, double delta_Y)
        {
            DeltaX = delta_X;
            DeltaY = delta_Y;
        }

        //============================================================

        public ScreenChangeInfo(ScreenInfo info1, ScreenInfo info2)
        {
            var width1 = info1.Width;
            var width2 = info2.Width;

            DeltaX = width2 - width1;

            var height1 = info2.Height;
            var height2 = info2.Height;

            DeltaY = height2 - height1;
        }
    }
}
