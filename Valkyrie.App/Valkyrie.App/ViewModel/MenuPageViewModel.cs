using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Valkyrie.Graphics;

namespace Valkyrie.App.ViewModel
{
    public class MenuPageViewModel : INotifyPropertyChanged
    {
        /*---------------------------------
         * 
         * Constructor
         * 
         * ------------------------------*/

        public MenuPageViewModel()
        {
            deviceScreen_ = new Screen();
            ButtonHeight = (int)deviceScreen_.Height / 4;
        }

        //===========================================================================

        /*------------------------------------
         * 
         * Function indicating weather there 
         * is a saved state in memory
         * 
         * ---------------------------------*/

        internal bool savedStateExists_ = false;
        public bool SaveStateExists
        {
            get
            {
                string FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "save.xml");
                return File.Exists(FileName);
            }
            set
            {
                savedStateExists_ = value;
            }
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
            get
            {
                return deviceScreen_;
            }
        }

        //---------------------------------

        internal Screen.Orientation orientation_; 
        public Screen.Orientation Orientation
        {
            get
            {
                return orientation_;
            }
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

        internal int buttonHeight;

        public event PropertyChangedEventHandler PropertyChanged;

        //==============================================================================

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

        //==============================================================================

        public int ButtonHeight
        {
            get
            {
                return buttonHeight;
            }
            set
            {
                buttonHeight = value;
            }
        }
    }
}
