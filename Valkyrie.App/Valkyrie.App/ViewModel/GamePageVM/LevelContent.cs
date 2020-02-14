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

                /*
                GLPosition scrollBoxOrigin = ScrollBox.GLRect.Origin;
                GLPosition obstacleOrigin = obstacle.GLObs.Rectangle.Origin;

                float deltaY = scrollBoxOrigin.Y - obstacleOrigin.Y;
                float deltaX = scrollBoxOrigin.X - obstacleOrigin.X;

                obstacle.Tiles.Translate(deltaX, deltaY);
                */

                float newY = (float)DeviceScreen.Height - obstacle.obstacle_.Rectangle.Top;

                float deltaY = obstacle.Tiles.SKOrigin.Y - newY;

                obstacle.Tiles.Translate(0.0f, deltaY);
            }

            // update all sprites

            foreach (var sprite in deviceScreen_.Sprites)
            {

            }
        }
    }
}
