using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace Valkyrie.App.ViewModel
{
    public class DevOptionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
