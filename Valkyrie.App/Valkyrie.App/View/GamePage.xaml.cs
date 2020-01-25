using System;
using Valkyrie.App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Valkyrie.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        internal GamePageViewModel gpvm_;

        internal String runtimePlatform_;
        public String RuntimePlatform 
        {
            get
            {
                return runtimePlatform_;
            }
        }

        //===================================================================

        /*-------------------------------------
         * 
         * Constructor
         * 
         * -----------------------------------*/

        public GamePage()
        {
            InitializeComponent();
            
            runtimePlatform_ = Device.RuntimePlatform;
            if(runtimePlatform_ == Device.UWP)
            {

            }

            gpvm_ = new GamePageViewModel();
            BindingContext = gpvm_;
        }

        //===================================================================

        /*-------------------------------------
         * 
         * Event Handler for a click on the 
         * paused button
         * 
         * -----------------------------------*/

        private void PauseButtonClicked(object sender, EventArgs e)
        {
            if(gpvm_.Paused)
            {
                gpvm_.Paused = false;
                return;
            }

            if(!gpvm_.Paused)
            {
                gpvm_.Paused = true;
                return;
            }

        }
    }
}