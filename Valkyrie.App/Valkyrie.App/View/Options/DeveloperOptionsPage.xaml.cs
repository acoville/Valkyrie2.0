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
            BindingContext = dovm_;
            InitializeComponent();

            FPS_switch.IsToggled = Preferences.Get("display_FPS", false);
            Runtime_Env_Switch.IsToggled = Preferences.Get("displayEnv", false);
            Scrollbox_Switch.IsToggled = Preferences.Get("displayScrollbox", false);
        }

        //=====================================================================

        protected override void OnSizeAllocated(double width, double height)
        {
            dovm_.screen_.GetScreenDetails();

            base.OnSizeAllocated(width, height);
        }

        //=====================================================================

        private void FPS_switch_Toggled(object sender, ToggledEventArgs e)
        {
            dovm_.DisplayFPS = e.Value;
        }

        //-------------------------------------------------------------------

        private void Runtime_Env_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            dovm_.DisplayRuntimeEnv = e.Value;
        }

        //-------------------------------------------------------------------

        private void Scrollbox_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            dovm_.DisplayScrollbox = e.Value;
        }

        //-----------------------------------------------------------------

        private void Coordinates_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            dovm_.DisplayCaptions = e.Value;
        }
    }
}