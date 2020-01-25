using System;
using Valkyrie.App.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Valkyrie.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        internal MenuPageViewModel menuPageViewModel_;
        
        internal OptionsPage optionsPage_;
        internal GamePage currentGame_;

        //==============================================================

        /*---------------------------------
         * 
         *  Constructor
         * 
         * --------------------------------*/

        public MenuPage()
        {
            InitializeComponent();

            menuPageViewModel_ = new MenuPageViewModel();
            BindingContext = menuPageViewModel_;
            BackgroundImageSource = menuPageViewModel_.GetImageSource();
            optionsPage_ = new OptionsPage();
        }

        //=============================================================

        /*--------------------------------
         * 
         * Event to update the background
         * image if the device orientation
         * changes
         * 
         * ------------------------------*/

        protected override void OnSizeAllocated(double width, double height)
        {
            menuPageViewModel_.DeviceScreen.GetScreenDetails();
            BackgroundImageSource = menuPageViewModel_.GetImageSource();
            menuPageViewModel_.ButtonHeight = (int)menuPageViewModel_.DeviceScreen.Height / 4;
            base.OnSizeAllocated(width, height);
        }

        //=============================================================

        /*----------------------------------
         * 
         * New Game OnClick
         * 
         * -------------------------------*/

        private void NewgameClicked(object sender, EventArgs e)
        {
            currentGame_ = new GamePage();
            Navigation.PushAsync(currentGame_);

            // enables other buttons

            Save_Btn.IsEnabled = true;
            Resume_Btn.IsEnabled = true;
            Options_Btn.IsEnabled = true;
        }

        //============================================================

        /*---------------------------------
         * 
         * Save Button Clicked
         * 
         * ------------------------------*/

        private void Save_Btn_Clicked(object sender, EventArgs e)
        {

        }

        //==========================================================

        /*--------------------------------
         * 
         * Load Button Clicked
         * 
         * -----------------------------*/
        private void Load_Btn_Clicked(object sender, EventArgs e)
        {

        }

        //=========================================================

        /*-----------------------------
         * 
         * Resume Button Clicked
         * 
         * --------------------------*/

        private void Resume_Btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(currentGame_);
        }

        //===========================================================

        /*-----------------------------
         * 
         * Options Menu Button Clicked
         * 
         * --------------------------*/

        private void Options_Btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(optionsPage_);
        }
    }
}