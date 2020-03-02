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
    public partial class DeveloperOptionsPage : ContentPage
    {
        internal DevOptionsViewModel dovm_;

        //=====================================================================

        public DeveloperOptionsPage()
        {
            dovm_ = new DevOptionsViewModel();
            InitializeComponent();
            FPS_switch.IsToggled = Preferences.Get("display_FPS", false);
        }

        //=====================================================================

        /*---------------------------
         * 
         * Toggle switch handler for 
         * displaying FPS 
         * 
         * ------------------------*/

        private void FPS_switch_Toggled(object sender, ToggledEventArgs e)
        {
            dovm_.DisplayFPS = e.Value;
        }
    }
}