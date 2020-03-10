using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Valkryie.GL;

namespace Valkyrie.App.Model
{
    public class Position
    {
        internal GLPosition glPosition_;
        public GLPosition GLPosition
        {
            get => glPosition_;
            set => glPosition_ = value;
        }

        //============================================================

        internal SKPoint skiaPosition_;
        public SKPoint SKPosition
        {
            get => skiaPosition_;
            set => skiaPosition_ = value;
        }

        //============================================================

        public Position()
        {
            glPosition_ = new GLPosition();
            skiaPosition_ = new SKPoint();
        }

        //===========================================================

        public Position(GLPosition gl, SKPoint sk)
        {
            glPosition_ = gl;
            skiaPosition_ = sk;
        }


    }
}
