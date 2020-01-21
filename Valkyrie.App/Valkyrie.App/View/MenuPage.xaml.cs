using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        internal GamePage currentGamePage_;

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

            //------------------------------------------------

            // Xamarin.Forms.ImageSource BackgroundImageSource { get; set; }

            BackgroundImageSource = menuPageViewModel_.GetImageSource();
        }

        //=============================================================

        /*----------------------------------
         * 
         * New Game OnClick
         * 
         * -------------------------------*/

        private void NewgameClicked(object sender, EventArgs e)
        {
            
        }
    }
}