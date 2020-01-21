using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Valkyrie.Graphics;
using Valkyrie.App.ViewModel;

namespace Valkyrie.App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        SplashScreenViewModel ssvm_;

        public SplashPage()
        {
            ssvm_ = new SplashScreenViewModel();
            InitializeComponent();
            BindingContext = ssvm_;
            BackgroundImageSource = ssvm_.ImageSource;
        }
    }
}