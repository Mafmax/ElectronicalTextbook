using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel
{
    public abstract class DialogViewModel<T> : ViewModel<T> where T : Window,new()
    {

        public bool? ShowAsDialog()
        {

            return window.ShowDialog();
        }
        public void Success()
        {
            window.DialogResult = true;
            window.Close();
        }
        protected DialogViewModel() : base()
        {

        }
    }
}
