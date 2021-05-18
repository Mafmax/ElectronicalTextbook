using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectronicalTextbook.ViewModel
{
    public class RegisterField<T> : RegisterFieldBase where T : Control
    {
        public T Content { get; set; }
        private Func<T, bool> isCorrectFunc;
        public RegisterField(Control control)
        {
            Label = (TextBlock)control.Template.FindName("label", control);
            Content = (T)control.Template.FindName("label", control);
            Error = (TextBlock)control.Template.FindName("error", control);
        }
        public void SetChecker(Func<T,bool> func)
        {
            isCorrectFunc = func;
        }

        public override bool IsCorrect()
        {
            bool correct = isCorrectFunc is null || isCorrectFunc(Content);
            if (!correct) Error.Text = "***";
            return correct;
        }
    }
}
