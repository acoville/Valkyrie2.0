using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Valkyrie.App.View;
using System.Threading.Tasks;

namespace Valkyrie.App
{
    public partial class App : Application
    {
        public MenuPage MainMenu;

        //======================================================

        /*--------------------------------
         * 
         * Constructor
         * also seems to be the entry 
         * point of the application
         * 
         * ------------------------------*/

        public App()
        {
            InitializeComponent();

            MainMenu = new MenuPage();

            //MainPage = new NavigationPage(MainMenu);

            MainPage = new NavigationPage(new SplashPage());
        }

        //=====================================================
        protected async override void OnStart()
        {
            await Task.Delay(TimeSpan.FromSeconds(4));
            MainMenu = new MenuPage();
            MainPage = new NavigationPage(MainMenu);
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
