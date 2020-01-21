using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Valkyrie.App.View;
using System.Threading.Tasks;

namespace Valkyrie.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SplashPage());
        }

        //=====================================================
        protected async override void OnStart()
        {
            await Task.Delay(TimeSpan.FromSeconds(4));
            MainPage = new NavigationPage(new MenuPage());
        }

        //=====================================================

        protected override void OnSleep()
        {
        }

        //=====================================================

        protected override void OnResume()
        {
        }
    }
}
