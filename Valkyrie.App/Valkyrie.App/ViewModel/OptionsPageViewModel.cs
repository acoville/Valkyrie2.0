using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Valkyrie.Graphics;
using Xamarin.Essentials;

namespace Valkyrie.App.ViewModel
{
    public class OptionsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //====================================================================

        internal bool keyboardPresent_ = false;
        public bool KeyboardPresent
        {
            get => keyboardPresent_;
        }

        //====================================================================

        /*--------------------------------------
         * 
         * Gets information about the screen
         * so we can use the right background
         * image.
         * 
         * -----------------------------------*/

        internal Screen deviceScreen_;
        public Screen DeviceScreen
        {
            get => deviceScreen_;
        }

        //====================================================================

        public double controlOpacity
        {
            get
            {
                return Preferences.Get("controlOpacity", 0.85);
            }
            set
            {
                if (Preferences.Get("controlOpacity", 0.85) == value)
                    return;

                Preferences.Set("controlOpacity", value);
                RaisePropertyChanged();
            }
        }

        //=================================================================

        /*------------------------------------------
         * 
         * Event Handler to raise propertyChanged
         * 
         * ---------------------------------------*/

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        //=================================================================

        /*--------------------------------------
         * 
         * Constructor
         * 
         * ------------------------------------*/

        public OptionsPageViewModel()
        {
            deviceScreen_ = new Screen();
            
        }

        //=======================================================================

        /*---------------------------------
         * 
         *  Helper function to determine 
         *  current screen orientation, 
         *  and therefore which image to use
         * 
         * ----------------------------------*/
        public String GetImageSource()
        {
            string filename;

            Graphics.Screen.Orientation orientation = DeviceScreen.ScreenOrientation;

            switch (orientation)
            {
                case (Graphics.Screen.Orientation.landscape):
                    {
                        filename = "menu_landscape.png";
                        break;
                    }

                case (Graphics.Screen.Orientation.portrait):
                    {
                        filename = "menu_portrait.png";
                        break;
                    }

                default:
                    {
                        filename = "menu_square.png";
                        break;
                    }
            }

            return filename;
        }
    }
}
