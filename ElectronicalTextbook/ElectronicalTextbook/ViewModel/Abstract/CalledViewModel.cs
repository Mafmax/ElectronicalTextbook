using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel
{
    public abstract class CalledViewModel<T> : ViewModel<T> where T:Window,new()
    {
        private event RoutedEventHandler OnEnd;
        public Window Caller { get; }
  
        protected CalledViewModel(Window caller)
        {

            Caller = caller;
            window.Closing += OnClose;
            
        }
        public virtual void Open()
        {
            Caller.Visibility = Visibility.Hidden;
            window.Show();
        }
        private void OnClose(object sender, CancelEventArgs e)
        {
            OnEnd?.Invoke(this, new RoutedEventArgs());
            Caller.Visibility = Visibility.Visible;
            Caller.Activate();
        }
        public virtual CalledViewModel<T> SetCallback(RoutedEventHandler callback)
        {
            OnEnd += callback;
            return this;
        }
        public virtual CalledViewModel<T> Init(object value)
        {
            return this;
        }
    }
}
