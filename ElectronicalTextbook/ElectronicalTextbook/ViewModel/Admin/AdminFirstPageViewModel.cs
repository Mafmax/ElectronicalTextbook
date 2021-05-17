using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ElectronicalTextbook.Model.Supported;
using ElectronicalTextbook.View.Admin;
namespace ElectronicalTextbook.ViewModel.Admin
{
    public class AdminFirstPageViewModel : CalledViewModel<FirstPageWindow>
    {
        public AdminFirstPageViewModel(Window caller) : base(caller)
        {
            window.newPassword.PasswordChanged += OnNewPasswordChanged;
            window.confirmPassword.PasswordChanged += OnConfirmPasswordChanged;
            window.confirm.Click += OnConfirmButtonClick;
            window.exit.Click += OnExitClick;
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnNewPasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckPasswords();
        }

        private void OnConfirmPasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckPasswords();

        }

        private void CheckPasswords()
        {
            string newPas = window.newPassword.Password;
            string confirmPas = window.confirmPassword.Password;
            window.confirm.IsEnabled = PasswordChecker
                .IsCorrectNew(newPas, confirmPas, out string success);
            window.error.Text = success;
        }

        private void OnConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            string oldPassword = window.oldPassword.Password;
            string newPassword = window.newPassword.Password;
            if (DataBaseProcessor.IsCorrectPassword("admin", oldPassword))
            {
                DataBaseProcessor.ChangePassword("admin", newPassword);
            }
            else
            {
                window.error.Text = "Старый пароль не верен";
            }
        }

    }
}
