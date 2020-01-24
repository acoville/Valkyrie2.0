using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Valkyrie.Graphics;
using Xamarin.Essentials;

namespace Valkyrie.App.ViewModel
{
    public class OptionsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
     
        internal Screen deviceScreen_;
        public Screen DeviceScreen
        {
            get
            {
                return deviceScreen_;
            }
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
    }
}
