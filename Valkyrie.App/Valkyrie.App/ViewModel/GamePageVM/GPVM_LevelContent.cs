using System.Collections.Generic;
using System.ComponentModel;
using Valkyrie.App.Model;
using Valkyrie.Controls;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        internal List<Actor> actors_;
        internal List<Obstacle> obstacles_;
        internal List<Prop> props_;

        internal List<IController> controllers_;

        // items
        // events
    }
}
