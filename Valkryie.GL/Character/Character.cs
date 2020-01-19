using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.GL
{
    public partial class Character
    {
        //=============================================

        /*----------------------------
         * 
         * Constructors
         * 
         * -------------------------*/
        public Character()
        { }

        //--------------------------------

        public Character(string L)
        {
            Name = L;
        }

        //--------------------------------

        // copy constructor, used during GPVM.AddMonsters()

        public Character(Character orig)
        {
            Name = orig.Name;
            HP = orig.HP;
            MaxHP = orig.MaxHP;

            xSpeed = orig.xSpeed;
            Max_X_Speed = orig.Max_X_Speed;
            xAccelerationRate = orig.xAccelerationRate;

            ySpeed = orig.ySpeed;
        }
    }
}
