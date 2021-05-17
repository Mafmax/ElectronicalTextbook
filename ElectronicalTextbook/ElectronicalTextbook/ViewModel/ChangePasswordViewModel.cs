using ElectronicalTextbook.Model.Supported;
using ElectronicalTextbook.View;
using ElectronicalTextbook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel
{
    public class ChangePasswordViewModel : CalledViewModel<ChangePasswordWindow>
    {
        public ChangePasswordViewModel(Window caller) : base(caller)
        {
            window.newPassword.PasswordChanged += OnNewPasswordChanged;
            window.confirmPassword.PasswordChanged += OnConfirmPasswordChanged;
        }
        public override CalledViewModel<ChangePasswordWindow> Init(object value)
        {
            //TODO: проинициализировать имя пользователя
            return this;
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
            string newPassword = window.newPassword.Password;
            string username = window.user.SelectedItem.ToString();
            if (!DataBaseProcessor.IsUsernameAvaliable(username))
            {
                DataBaseProcessor.ChangePassword(username, newPassword);
                Close();
            }
            else
            {
                window.error.Text = "Пользователь не найден";
            }

        }
    }
}
