using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Valkyrie.App.Model;
using Valkyrie.Controls;

namespace Valkyrie.App.ViewModel
{
    public partial class GamePageViewModel : INotifyPropertyChanged
    {
        internal ConcurrentBag<IController> controllers_ = new ConcurrentBag<IController>();
        internal ConcurrentBag<Actor> actors_ = new ConcurrentBag<Actor>();
        internal ConcurrentBag<Obstacle> obstacles_ = new ConcurrentBag<Obstacle>();
        internal ConcurrentBag<Prop> props_ = new ConcurrentBag<Prop>();


        // items
        // events
    }
}
