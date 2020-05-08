using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Valkyrie.Graphics;
using Xamarin.Forms;

namespace Valkyrie.App.ViewModel
{
    public class ControlOptionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //================================================================

        internal Screen deviceScreen_;
        public Screen DeviceScreen
        {
            get => deviceScreen_;
            set => deviceScreen_ = value;
        }

        //====================================================================

        internal bool keyboardPresent_ = false;
        public bool KeyboardPresent
        {
            get => keyboardPresent_;
        }

        //====================================================================

        internal bool gamepadPresent_ = false;
        public bool GamepadPresent
        {
            get => gamepadPresent_;
        }

        //=========================================================================

        /*--------------------------------
         * 
         * Datasource of the input methods 
         * ListView  
         *
         * ------------------------------*/

        internal List<string> controllers_;
        public List<string> Controllers
        {
            get => controllers_;
        }

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

        //======================================================================

        public ControlOptionsViewModel()
        {
            deviceScreen_ = new Screen();

            controllers_ = new List<string>
            {
                "Virtual Gamepad"
            };

            DetectControllers();

            if(keyboardPresent_)
            {
                controllers_.Add("Keyboard + Mouse");
            }

            if(gamepadPresent_)
            {
                controllers_.Add("Gamepad");
            }
        }

        //=====================================================================

        internal void DetectControllers()
        {
            keyboardPresent_ = DetectKeyboard();
            gamepadPresent_ = DetectGamepad();
        }

        //====================================================================

        internal bool DetectKeyboard()
        {


            return true;
        }

        //====================================================================

        internal bool DetectGamepad()
        {
            return true;
        }
            
        //=====================================================================

        public string GetImageSource()
        {
            string filename;

            Graphics.Screen.Orientation orientation = deviceScreen_.ScreenOrientation;

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

        //============================================================================

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
