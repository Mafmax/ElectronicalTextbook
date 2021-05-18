using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectronicalTextbook.ViewModel
{
    public abstract class RegisterFieldBase
    {
        public TextBlock Label { get; set; }
        public TextBlock Error { get; set; }
        

        public abstract bool IsCorrect();
    }
}
