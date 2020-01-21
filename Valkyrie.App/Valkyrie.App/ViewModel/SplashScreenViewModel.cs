using System;
using System.Collections.Generic;
using System.Text;
using Valkyrie.Graphics;

namespace Valkyrie.App.ViewModel
{
    public class SplashScreenViewModel
    {
        internal Screen deviceScreen_;
        internal String imageSource_;
        public String ImageSource
        {
            get
            {
                return imageSource_;
            }
        }

        //================================================================

        /*-----------------------------------------
         * 
         * This view model's only job is to 
         * determine the native display 
         * information and select the appropriate
         * image. 
         * 
         * --------------------------------------*/

        public SplashScreenViewModel()
        {
            deviceScreen_ = new Screen();

            Screen.Orientation orientation = deviceScreen_.ScreenOrientation;

            switch (orientation)
            {
                case (Screen.Orientation.landscape):
                {
                    imageSource_ = "splash_landscape.png";
                    break;
                }

                case (Screen.Orientation.portrait):
                {
                    imageSource_ = "splash_portrait.png";
                    break;
                }

                default:
                {
                    imageSource_ = "splash_portrait.png";
                    break;
                }
            }
        }
    }
}
