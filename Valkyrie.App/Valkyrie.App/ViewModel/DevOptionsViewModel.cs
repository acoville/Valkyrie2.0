using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Valkyrie.App.ViewModel
{
    public class DevOptionsViewModel : INotifyPropertyChanged
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

        //==================================================================

        public DevOptionsViewModel()
        {
        }

        //=================================================================

        public bool DisplayFPS
        {
            get
            {
                return Preferences.Get("display_FPS", false);
            }

            set
            {
                Preferences.Set("display_FPS", value);
                RaisePropertyChanged(nameof(DisplayFPS));
            }
        }

        //=================================================================

        public bool DisplayRuntimeEnv
        {
            get
            {
                return Preferences.Get("displayEnv", false);
            }

            set
            {
                Preferences.Set("displayEnv", value);
                RaisePropertyChanged(nameof(DisplayRuntimeEnv));
            }
        }

        //===================================================================

        public bool DisplayScrollbox
        {
            get
            {
                return Preferences.Get("displayScrollbox", false);
            }
            set
            {
                Preferences.Set("displayScrollbox", value);
                RaisePropertyChanged(nameof(DisplayScrollbox));
            }
        }

        //=================================================================

        /*----------------------------
         * 
         * Event Handler for 
         * Property Changed
         * 
         * --------------------------*/

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
