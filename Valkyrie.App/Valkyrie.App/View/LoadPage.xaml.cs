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
    public partial class LoadPage : ContentPage
    {
        internal LoadPageViewModel lpvm_;

        //============================================================

        /*----------------------------------
         * 
         * Constructor
         * 
         * -------------------------------*/

        public LoadPage()
        {
            InitializeComponent();
        }
    }
}