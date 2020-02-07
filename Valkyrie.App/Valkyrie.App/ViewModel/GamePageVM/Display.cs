﻿

using System.ComponentModel;
using Valkryie.GL;
using Valkyrie.App.Model;
using Valkyrie.Graphics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        //============================================================

        /*--------------------------------------
         * 
         * We do quite a lot of work with the 
         * Screen and Sprite classes here
         * 
         * ------------------------------------*/

        internal GameScreen deviceScreen_;
        public GameScreen DeviceScreen
        {
            get => deviceScreen_;
        }

        //===============================================================

        /*-------------------------------------------------
         * 
         * This is a set of dynamic GL coordinates which
         * correspond to fixed SK coordinates. If the 
         * player leaves this box, the camera follows
         * 
         * -----------------------------------------------*/

        internal ScrollBox scrollBox_;
        public ScrollBox ScrollBox
        {
            get => scrollBox_;
            set => scrollBox_ = value;
        }

        //===============================================================

        //----------- control buttons opacity

        internal double controlOpacity_ = Preferences.Get("controlOpacity", 0.85);
        public double ControlOpacity
        {
            get => controlOpacity_;
        }

        //==============================================================

        // Background Image streams from the GameScreen 

        internal ImageSource backgroundImage_;
        public ImageSource BackgroundImage
        {
            get => backgroundImage_;
            set => backgroundImage_ = value;
        }

        //==============================================================

        /*-----------------------------------
         * 
         * Troubleshooting information
         * Raw frames tracking info 
         * used to calculate FPS
         * 
         * ---------------------------------*/

        internal int frames_;
        public int Frames
        {
            get => frames_;

            set
            {
                if(value == 0)
                {
                    FPS = 1000/frames_;
                }

                frames_ = value;
            }
        }


        //==============================================================

        /*------------------------------------
         * 
         * Troubleshooting Information 
         *  Framerate
         * 
         * ----------------------------------*/

        internal int fps_;
        public int FPS
        {
            get => fps_;

            set
            {
                fps_ = value;
                RaisePropertyChanged();
            }
        }

        //=============================================================

        /*----------------------------------
         * 
         * Troubleshooting Information
         * Runtime Environment
         * 
         * ------------------------------*/

        internal string env_;
        public string Env
        {
            get => env_;
        }

        //=============================================================

        /*----------------------------------
         * 
         * Control variable displays debug
         * information on screen
         * 
         * -------------------------------*/

        internal bool troubleVisibile_ = true;
        public bool Trouble_Visible
        {
            get => troubleVisibile_;

            set
            {
                troubleVisibile_ = value;
                DeviceScreen.Trouble = value;
                RaisePropertyChanged();
            }
        }
    }
}