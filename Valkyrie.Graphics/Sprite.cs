/*========================================================
 * 
 * 
 * 
 * 
 * 
 * ======================================================*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.Graphics
{
    public delegate void DisplayImageChangedHandler(object sender, SpriteEventArgs e);

    public class Sprite : Drawable
    {

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
