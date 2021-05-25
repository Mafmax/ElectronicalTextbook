using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectronicalTextbook.Model.Controllers
{
    public abstract class Viewer
    {
        public abstract Panel View();
        protected abstract void SaveChanges();
        public event EventHandler OnClose;
        protected void Close(bool withSave)
        {
            if (withSave) SaveChanges();
            OnClose?.Invoke(this, new EventArgs());
        }

    }
}
