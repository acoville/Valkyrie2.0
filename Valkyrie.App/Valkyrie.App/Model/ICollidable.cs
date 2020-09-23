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

        //------------------------------------------------

        float Vertical_Distance_Below(ICollidable other);
        float Vertical_Distance_Above(ICollidable other);

        bool Is_Above(ICollidable other);
        bool Is_Below(ICollidable other);

        //-------------------------------------------------

        bool Is_Left_Of(ICollidable other);
        bool Is_Right_Of(ICollidable other);
    }
}
