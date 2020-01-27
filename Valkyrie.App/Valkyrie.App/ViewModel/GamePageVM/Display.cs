

using System.ComponentModel;
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

        internal Screen deviceScreen_;
        public Screen DeviceScreen
        {
            get => deviceScreen_;
        }

        //===============================================================

        //----------- control buttons opacity

        internal double controlOpacity_ = Preferences.Get("controlOpacity", 0.85);
        public double ControlOpacity
        {
            get => controlOpacity_;
        }
    }
}