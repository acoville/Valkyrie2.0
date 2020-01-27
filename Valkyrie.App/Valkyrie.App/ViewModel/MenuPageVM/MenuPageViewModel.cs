using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Valkyrie.Graphics;
using Xamarin.Forms;

namespace Valkyrie.App.ViewModel
{
    public class MenuPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //==========================================================================

        /*--------------------------------
         * 
         * Active and Inactive colors
         * for the menu buttons
         * 
         * -----------------------------*/

        internal Color activeColor_ = Color.LightBlue;
        public Color ActiveColor
        {
            get => activeColor_;
            set
            {
                activeColor_ = value;
                RaisePropertyChanged();
            }
        }

        //--------------------------------------------

        internal Color inactiveColor_ = Color.Gray;
        public Color InactiveColor
        {
            get => inactiveColor_;
            set
            {
                inactiveColor_ = value;
                RaisePropertyChanged();
            }
        }

        //==========================================================================

        internal int buttonHeight;
        public int ButtonHeight
        {
            get => buttonHeight;
            set
            {
                buttonHeight = value;
            }
        }

        //========================================={===================================

        /*---------------------------------
         * 
         * Constructor
         * 
         * ------------------------------*/

        public MenuPageViewModel()
        {
            deviceScreen_ = new Screen();
            mapLoader = new LevelLoader();
            ButtonHeight = (int)deviceScreen_.Height / 4;
        }

        //===========================================================================

        /*----------------------------------
         * 
         * Imported from Valkyrie.Graphics 
         * library so I can use its screen 
         * orientation detection to select
         * the correct background image
         * 
         * -------------------------------*/

        internal Screen deviceScreen_;
        public Screen DeviceScreen
        {
            get => deviceScreen_;
        }

        //=======================================================================

        /*--------------------------------
         * 
         * Level Loader 
         * 
         * -------------------------------*/

        internal LevelLoader mapLoader;

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

            switch(orientation)
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

        //==============================================================================

        /*------------------------------------------
         * 
         * Event Handler to raise propertyChanged
         * 
         * ---------------------------------------*/

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        //==============================================================================


    }
}
