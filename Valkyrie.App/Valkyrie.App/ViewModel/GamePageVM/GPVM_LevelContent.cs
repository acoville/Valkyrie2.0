using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Valkyrie.App.Model;
using Valkyrie.Controls;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        internal List<IController> controllers_;

        internal List<Actor> actors_;
        internal List<Obstacle> obstacles_;
        internal List<Prop> props_;


        // items
        // events
    }
}
