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
    }
}