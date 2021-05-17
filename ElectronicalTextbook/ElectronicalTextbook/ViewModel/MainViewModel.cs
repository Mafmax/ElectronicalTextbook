using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicalTextbook.ViewModel
{
    public class MainViewModel
    {
        public MainWindow Window { get; }

        public MainViewModel(MainWindow window)
        {
            Window = window;
            Window.registerBtn.Click += OnRegisterButtonClick;
            Window.enterBtn.Click += OnEnterButtonClick;
            Window.passwordRecoveryBtn.Click += OnPasswordRecoveryButtonClick;
            Window.adminBtn.Click += OnChangeAdminPasswordButtonClick;
         

        }
        private void Enter(string username, string password)
        {
            throw new NotImplementedException();
        }
        private void TryEnter(string username, string password)
        {
            bool correct = DataBaseProcessor.IsCorrectPassword(username, password);
            Window.errorText.Visibility = Visibility.Hidden;
            if (correct) Enter(username, password);
            else Window.errorText.Visibility = Visibility.Visible;
        }
        private void OnEnterButtonClick(object sender, RoutedEventArgs e)
        {
            string username = Window.loginTxt.Text;
            string password = Window.passwordTxt.Password;
            TryEnter(username, password);
        }
        private void OnChangeAdminPasswordButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void OnPasswordRecoveryButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void OnRegisterButtonClick(object sender, RoutedEventArgs e)
        {

        }

    }
}
