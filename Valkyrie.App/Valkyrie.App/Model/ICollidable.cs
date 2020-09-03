using System;
using System.Collections.Generic;
using System.Text;
using Valkryie.GL;

namespace Valkyrie.App.Model
{
    public interface ICollidable
    {
        bool Intersects(ICollidable other);
        bool Contains(ICollidable other);

        GLRect Rectangle
        {
            get;
        }
    }
}
