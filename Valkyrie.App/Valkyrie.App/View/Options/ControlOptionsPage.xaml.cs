using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valkyrie.App.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Valkyrie.App.View.Options
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlOptionsPage : ContentPage
    {
        internal ControlOptionsViewModel covm_;

        //======================================================================

        public ControlOptionsPage()
        {
            InitializeComponent();

            covm_ = new ControlOptionsViewModel();
            BindingContext = covm_;
            BackgroundImageSource = covm_.GetImageSource();
        }

        //=====================================================================

        /*----------------------------------
         * 
         * A new input method has been 
         * selected
         * 
         * --------------------------------*/

        private void Selector_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selection = e.SelectedItem.ToString();

            switch (selection)
            {
                case ("Virtual Gamepad"):
                {
                    Preferences.Set("Controller", "Virtual Gamepad");
                    break;
                }

                //-----------------------------------------

                case ("Keyboard + Mouse"):
                {
                    Preferences.Set("Controller", "KBM");
                    break;
                }

                //-----------------------------------------

                case ("Gamepad"):
                {
                    Preferences.Set("Controller", "Gamepad");
                    break;
                }
            }
        }
    }
}