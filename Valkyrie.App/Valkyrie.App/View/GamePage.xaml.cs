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


        //================================================================

        /*-------------------------------------
         * 
         * Constructor
         * 
         * -----------------------------------*/

        public GamePage()
        {
            InitializeComponent();
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