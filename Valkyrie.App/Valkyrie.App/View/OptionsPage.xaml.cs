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
        MenuPageViewModel menuPageViewModel;

        OptionsPage _options;

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

            menuPageViewModel = new MenuPageViewModel();
            BindingContext = menuPageViewModel;
            BackgroundImageSource = menuPageViewModel.GetImageSource();
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
            menuPageViewModel.DeviceScreen.GetScreenDetails();

            base.OnSizeAllocated(width, height);
        }
    }
}