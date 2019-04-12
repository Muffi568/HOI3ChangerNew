using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SharedData {
    public class ThreadingObservableCollection<T> : ObservableCollection<T> {
        Dispatcher _dispatcher;
        public ThreadingObservableCollection(Dispatcher dispatcher) {
            _dispatcher = dispatcher;
        }
        public new void Add(T item) {
            if (_dispatcher == null)
                base.Add(item);
            else
                _dispatcher.Invoke(new Action(() => base.Add(item)));
        }
    }
}
