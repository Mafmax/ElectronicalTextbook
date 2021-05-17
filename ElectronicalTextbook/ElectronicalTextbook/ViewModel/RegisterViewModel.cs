using ElectronicalTextbook.Model.Supported;
using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using  fieldType = System.ValueTuple<
    System.Windows.Controls.TextBlock,
    System.Windows.Controls.TextBox, 
    System.Windows.Controls.TextBlock>;
using  passType = System.ValueTuple<
    System.Windows.Controls.TextBlock,
    System.Windows.Controls.PasswordBox,
    System.Windows.Controls.TextBlock>;
namespace ElectronicalTextbook.ViewModel
{
    public abstract class RegisterViewModel : CalledViewModel<RegisterWindow>
    {
        protected fieldType login;
        protected fieldType name;
        protected fieldType lastname;
        protected fieldType surname;
        protected fieldType sex;

        protected passType password;
        protected passType confirmPassword;


        public RegisterViewModel(Window caller) : base(caller)
        {
            if (window.IsLoaded)
            {
                CommonUnpack(null, null);
            }
            else
            {
                window.Loaded += CommonUnpack;
            }
        }
        protected abstract void Register();
        protected abstract void SpecifiedUnpack();
        protected abstract void InitSpecifiedEvents();
        protected abstract void AddSpecified();
        private void CommonUnpack(object sender, RoutedEventArgs e)
        {
            string name1 = "field";
            string name2 = "value";
            string name3 = "error";
            var names = (name1, name2, name3);
            login = UnpackTemplatedSource<TextBlock, TextBox, TextBlock>(
                names, window.login);
            login.Item1.Text = "Логин: ";
            name = UnpackTemplatedSource<TextBlock, TextBox, TextBlock>(
                names, window.name);
            name.Item1.Text = "Имя: ";
            lastname = UnpackTemplatedSource<TextBlock, TextBox, TextBlock>(
                names, window.lastname);
            lastname.Item1.Text = "Фамилия: ";
            surname = UnpackTemplatedSource<TextBlock, TextBox, TextBlock>(
                names, window.surname);
            surname.Item1.Text = "Отчество: ";
            sex = UnpackTemplatedSource<TextBlock, TextBox, TextBlock>(
                names, window.sex);
            sex.Item1.Text = "Пол: ";
            password = UnpackTemplatedSource<TextBlock, PasswordBox, TextBlock>(
                names, window.password);
            password.Item1.Text = "Пароль: ";
            confirmPassword = UnpackTemplatedSource<TextBlock, PasswordBox, TextBlock>(
                names, window.confirmPassword);
            confirmPassword.Item1.Text = "Повторно пароль: ";
            AddSpecified();
            SpecifiedUnpack();
            InitCommonEvents();
        }

        private void InitCommonEvents()
        {
            login.Item2.TextChanged += OnLoginChanged;
            name.Item2.TextChanged += OnNameChanged;
            lastname.Item2.TextChanged += OnLastnameChanged;
            surname.Item2.TextChanged += OnSurnameChanged;
            password.Item2.PasswordChanged += OnPasswordChanged;
            confirmPassword.Item2.PasswordChanged += OnConfirmPasswordChanged;
            sex.Item2.TextChanged += OnSexChanged;
            window.registerButton.Click += OnRegisterButtonClick;
            InitSpecifiedEvents();
        }

        private void OnRegisterButtonClick(object sender, RoutedEventArgs e)
        {
            Register();
        }

        private void OnSexChanged(object sender, TextChangedEventArgs e)
        {
            CheckSex(sex);
        }

        protected void CheckSex((TextBlock label, TextBox value, TextBlock error) pair)
        {
            window.registerButton.IsEnabled = CheckSex(pair.value.Text, out var message);
            pair.error.Text = message;
        }
        protected bool CheckSex(string value, out string message)
        {
            message = "";
            if (string.IsNullOrEmpty(value))
            {
                message = "Поле не может быть пустым";
                return false;
            }
            if (!Regex.IsMatch(value, @"[мМжЖ]"))
            {
                message = "Буквой М или Ж";
                return false;
            }
            return true;
        }
        protected void CheckLogin(fieldType pair)
        {
            window.registerButton.IsEnabled = CheckLogin(pair.Item2.Text, out var message);
            pair.Item3.Text = message;
        }
        protected bool CheckLogin(string value, out string message)
        {
            message = "";
            if (string.IsNullOrEmpty(value))
            {
                message = "Поле не может быть пустым";
                return false;
            }
            if (value.Length < 4)
            {
                message = "Минимум 4 символа";
                return false;
            }
            if (!Regex.IsMatch(value, @"[a-zA-Z0-9]"))
            {
                message = "Только символы латинского алфавита и цифры";
                return false;
            }
            return true;
        }
        protected void CheckNameLastnameSurname(fieldType pair)
        {
            window.registerButton.IsEnabled = CheckNameLastnameSurname(pair.Item2.Text, out var error);
            pair.Item1.Text = error;
        }
        protected bool CheckNameLastnameSurname(string value, out string message)
        {
            message = "";
            if (string.IsNullOrEmpty(value))
            {
                message = "Поле не может быть пустым";
                return false;
            }
            if (value.Length < 2)
            {
                message = "Слишком коротко";
                return false;
            }
            if (!Regex.IsMatch(value, @"[а-яА-Я]"))
            {
                message = "Только буквы русского алфавита";
                return false;
            }
            return true;
        }

        private void OnConfirmPasswordChanged(object sender, RoutedEventArgs e)
        {
            window.registerButton.IsEnabled = PasswordChecker.IsCorrectNew(password.Item2.Password,
             confirmPassword.Item2.Password, out var message);
            confirmPassword.Item3.Text = message;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            window.registerButton.IsEnabled = PasswordChecker
                .IsCorrect(password.Item2.Password, out var message);
            password.Item3.Text = message;
        }

        private void OnSurnameChanged(object sender, TextChangedEventArgs e)
        {
            CheckNameLastnameSurname(surname);
        }

        private void OnLastnameChanged(object sender, TextChangedEventArgs e)
        {
            CheckNameLastnameSurname(lastname);
        }

        private void OnNameChanged(object sender, TextChangedEventArgs e)
        {
            CheckNameLastnameSurname(name);
        }

        private void OnLoginChanged(object sender, TextChangedEventArgs e)
        {
            CheckLogin(login);
        }

        protected static (T1, T2) UnpackTemplatedSource<T1, T2>((string name1, string name2) names, Control templatedSource)
        {
            var a = (T1)templatedSource.Template.FindName(names.name1, templatedSource);
            var b = (T2)templatedSource.Template.FindName(names.name2, templatedSource);
            return (a, b);
        }
        protected static (T1, T2, T3) UnpackTemplatedSource<T1, T2, T3>((string name1, string name2, string name3) names, Control templatedSource)
        {
            var a = (T1)templatedSource.Template.FindName(names.name1, templatedSource);
            var b = (T2)templatedSource.Template.FindName(names.name2, templatedSource);
            var c = (T3)templatedSource.Template.FindName(names.name3, templatedSource);
            return (a, b, c);
        }

    }
}
