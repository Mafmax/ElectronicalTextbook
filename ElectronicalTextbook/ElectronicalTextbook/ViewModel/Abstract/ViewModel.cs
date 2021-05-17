using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel
{
    public abstract class ViewModel<T> where T :  Window,new()
    {
        protected readonly T window;
        public ViewModel()
        {
            window = new T();
        }
        protected void Close()
        {
            window.Close();
        }
    }
}
