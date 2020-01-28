

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

        internal GameScreen deviceScreen_;
        public GameScreen DeviceScreen
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

        //==============================================================

        // Background Image streams from the GameScreen 

        internal ImageSource backgroundImage_;
        public ImageSource BackgroundImage
        {
            get => backgroundImage_;
            set
            {
                backgroundImage_ = value;
            }
        }
    }
}