using System;
using System.Collections.Generic;
using System.Text;
using static Valkyrie.Graphics.Screen;

namespace Valkyrie.Graphics
{
    public class ScreenInfo
    {
        internal Orientation orientation_;
        public Orientation Orientation
        {
            get => orientation_;
        }

        //---------------------------------------------

        internal double height_;
        public double Height
        {
            get => height_;
        }

        //---------------------------------------------

        internal double width_;
        public double Width
        {
            get => width_;
        }

        //============================================================

        public ScreenInfo(double h, double w)
        {
            height_ = h;
            width_ = w;

            // shrink the render area to accomodate the 
            // virtual controls, D-Pad and actionbuttons

            height_ *= .75;

            // screen is currently in portrait

            if (Height > Width)
            {
                orientation_ = Orientation.portrait;
            }

            // screen is currently in landscape

            else if (Height < Width)
            {
                orientation_ = Orientation.landscape;
            }

            // must be a square

            else
            {
                orientation_ = Orientation.square;
            }
        }
    }
}
