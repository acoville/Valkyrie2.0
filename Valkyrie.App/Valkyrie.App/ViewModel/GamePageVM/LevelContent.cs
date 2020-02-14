using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Valkryie.GL;
using Valkyrie.App.Model;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {


        //============================================================

        /*------------------------------------
         * 
         * Actors
         * 
         * --------------------------------*/

        internal List<Actor> actors_;


        //===========================================================

        /*------------------------------------
         * 
         * Obstacles
         * 
         * ---------------------------------*/

        internal List<Obstacle> obstacles_;


        //===========================================================

        /*--------------------------------
         * 
         * Events? 
         * 
         * -----------------------------*/



        //===========================================================

        /*-----------------------------
         * 
         * Items?
         * 
         * ---------------------------*/

        //=================================================================

        /*-------------------------------
         * 
         * Helper function to update  
         * on-screen elements based
         * on the scrollbox
         * 
         * -----------------------------*/

        internal void AlignGamePiecesToScrollBox()
        {
            // update all tiles

            foreach (var obstacle in obstacles_)
            {
                // step 1: find out far from the scrollbox GL origin 
                //      this obstacle's GL origin is

                


                //obstacle.Tiles.
            }

            // update all sprites

            foreach (var sprite in deviceScreen_.Sprites)
            {

            }
        }
    }
}
