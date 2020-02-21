using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.Graphics
{
    public class SpriteEventArgs : EventArgs
    {
        public SpriteEventArgs(object sender, Status status, Facing facing)
        {
            var Sprite = sender as Sprite;

            
        }
    }
}
