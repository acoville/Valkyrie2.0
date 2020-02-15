using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Valkyrie.App.ViewModel;
using Xamarin.Essentials;

namespace Valkyrie.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        internal OptionsPageViewModel opvm_;

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
            Preferences.Set("ControlOpacity", opacityController.Value);
        }
    }
}