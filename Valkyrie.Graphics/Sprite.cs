/*========================================================
 * 
 * 
 * 
 * 
 * 
 * ======================================================*/

using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Valkyrie.GL;

namespace Valkyrie.Graphics
{
    public delegate void DisplayImageChangedHandler(object sender, SpriteEventArgs e);

    public class Sprite : Drawable
    {
        internal SKBitmap standingImage_;
        public SKBitmap StandingImage
        {
            get => standingImage_;
            set => standingImage_ = value;
        }

        //----------------------------------------

        internal SKBitmap fallingImage_;
        public SKBitmap FallingImage
        {
            get => fallingImage_;
            set => fallingImage_ = value;
        }

        //-----------------------------------------

        internal SKBitmap crouchingImage_;
        public SKBitmap CrouchingImage
        {
            get => crouchingImage_;
            set => crouchingImage_ = value;
        }

        //----------------------------------------

        internal SKBitmap attackImage_;
        public SKBitmap AttackImage
        {
            get => attackImage_;
            set => attackImage_ = value;
        }

        //==============================================================

        internal Facing facing_ = Facing.right;
        public Facing Facing
        {
            get => facing_;
            set
            {
                if(value != facing_)
                {
                    Mirror();
                }

                facing_ = value;
            }
        }

        //----------------------------------------

        internal Status status_ = Status.standing;
        public Status Status
        {
            get => status_;

            set
            {
                switch (value)
                {
                    case (Status.standing):
                    {
                        if(!standingImage_.IsNull)
                        {
                            DisplayImage = StandingImage;
                        }
                        
                        break;
                    }

                    //-------------------------------------------

                    case (Status.falling):
                    {
                        if(!fallingImage_.IsNull)
                        {
                            DisplayImage = FallingImage;
                        }
                        
                        break;
                    }

                    //------------------------------------------

                    case (Status.attack):
                    {
                        if(!attackImage_.IsNull)
                        {
                            DisplayImage = AttackImage;
                        }

                        break;
                    }

                    //-------------------------------------------

                    case (Status.crouching):
                    {
                        if(!crouchingImage_.IsNull)
                        {
                            DisplayImage = CrouchingImage;
                        }

                        break;
                    }
                }

                //-- all of those picture should be right facing
                //-- so check and see if we need to mirror the new display image

                if(facing_ != Facing.right)
                {
                    Mirror();
                }
            }
        }

        //==========================================================

        public Sprite()
        {}
    }

    //=============================================================

    //-- these correspond to the SideScroller.GL enums by the same name

    public enum Status
    {
        standing,
        crouching,
        falling,
        attack
    };

    public enum Facing { left, right };
}
