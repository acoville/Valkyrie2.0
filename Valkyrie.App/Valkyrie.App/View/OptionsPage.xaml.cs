using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Valkyrie.App.ViewModel;

namespace Valkyrie.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        internal OptionsPageViewModel opvm_;
        internal GamePage currentGame;

        //==================================================================

        /*-----------------------------
         * 
         * Constructor
         * 
         * --------------------------*/

        public OptionsPage()
        {
            InitializeComponent();

            opvm_ = new OptionsPageViewModel();
            BindingContext = opvm_;

        }

        //================================================================

        /*----------------------------------
         * 
         * Event to update the background
         * image if the device orientation 
         * changes
         * 
         * -------------------------------*/

        protected override void OnSizeAllocated(double width, double height)
        {
            opvm_.DeviceScreen.GetScreenDetails();
            BackgroundImageSource = opvm_.GetImageSource();
            base.OnSizeAllocated(width, height);
        }

        //===========================================================================

        /*-----------------------------------
         * 
         * Event Handler for Opacity Slider
         * 
         * --------------------------------*/

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            opvm_.controlOpacity = opacityController.Value;
            //currentGame.gpvm_.controlOpacity = opacityController.Value;
        }
    }
}