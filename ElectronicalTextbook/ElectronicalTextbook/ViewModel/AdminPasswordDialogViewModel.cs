using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel
{
    public class AdminPasswordDialogViewModel : DialogViewModel<AdminPasswordDialogWindow>
    {

        public AdminPasswordDialogViewModel()
        {
            window.confirm.Click += OnConfirmButtonClick;
            window.reject.Click += OnRejectButtonClick;
        }

        private void OnRejectButtonClick(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
        private void Enter(string password)
        {
            Success();
        }
        private void TryEnter(string password)
        {
            bool correct = DataBaseProcessor.IsCorrectPassword("admin", password);
            if (correct) Enter(password);
            else window.errorTxt.Visibility = Visibility.Visible;
        }
        private void OnConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            string password = window.password.Password;
            TryEnter(password);
        }

    }
}
