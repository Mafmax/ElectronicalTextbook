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

namespace ElectronicalTextbook.ViewModel
{
    public abstract class RegisterViewModel : CalledViewModel<RegisterWindow>
    {
        protected RegisterField<TextBox> login;
        protected RegisterField<TextBox> name;
        protected RegisterField<TextBox> lastname;
        protected RegisterField<TextBox> surname;
        protected RegisterField<ComboBox> sex;

        protected RegisterField<PasswordBox> password;
        protected RegisterField<PasswordBox> confirmPassword;
        protected List<RegisterFieldBase> fields = new List<RegisterFieldBase>();

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
        protected abstract IEnumerable<RegisterFieldBase> SpecifiedUnpack();
        protected abstract void InitSpecifiedEvents();
        protected abstract void AddSpecified();
        protected bool CheckAll()
        {
            bool isCorrectAll = true;
            foreach (var item in fields)
            {
                isCorrectAll = isCorrectAll && item.IsCorrect();
            }
            return isCorrectAll;
        }
        private void CommonUnpack(object sender, RoutedEventArgs e)
        {
            login = new RegisterField<TextBox>(window.login);
            login.Label.Text = "Логин: ";
            login.SetChecker(x => CheckLogin(x.Text, out var plug));
            name = new RegisterField<TextBox>(window.name);
            name.Label.Text = "Имя: ";
            name.SetChecker(x => CheckNameLastnameSurname(x.Text, out var plug));
            lastname = new RegisterField<TextBox>(window.lastname);
            lastname.Label.Text = "Фамилия: ";
            lastname.SetChecker(x => CheckNameLastnameSurname(x.Text, out var plug));
            surname = new RegisterField<TextBox>(window.surname);
            surname.Label.Text = "Отчество: ";
            surname.SetChecker(x => CheckNameLastnameSurname(x.Text, out var plug));
            sex = new RegisterField<ComboBox>(window.sex);
            sex.Label.Text = "Пол: ";
            sex.SetChecker(x => x.SelectedItem != null);
            password = new RegisterField<PasswordBox>(window.password);
            password.Label.Text = "Пароль: ";
            password.SetChecker(x => PasswordChecker.IsCorrect(x.Password, out var plug));
            confirmPassword = new RegisterField<PasswordBox>(window.confirmPassword);
            confirmPassword.Label.Text = "Повторно пароль: ";
            confirmPassword.SetChecker(x => PasswordChecker.IsCorrect(x.Password, out var plug));
            fields.Add(login);
            fields.Add(name);
            fields.Add(lastname);
            fields.Add(surname);
            fields.Add(sex);
            fields.Add(password);
            fields.Add(confirmPassword);
            AddSpecified();
            fields.AddRange(SpecifiedUnpack());
            InitCommonEvents();
            SetCommonStartValues();
        }
        protected abstract void SetSpecifiedStartValues();
        private void SetCommonStartValues()
        {
            login.Content.Text = "";
            name.Content.Text = "";
            lastname.Content.Text = "";
            surname.Content.Text = "";
            TextBlock m = new TextBlock();
            TextBlock zhh = new TextBlock();
            sex.Content.Items.Add(m);
            sex.Content.Items.Add(zhh);
            sex.Content.SelectedIndex = 0;

            SetSpecifiedStartValues();
        }
        private void InitCommonEvents()
        {
            login.Content.TextChanged += OnLoginChanged;
            name.Content.TextChanged += OnNameChanged;
            lastname.Content.TextChanged += OnLastnameChanged;
            surname.Content.TextChanged += OnSurnameChanged;
            password.Content.PasswordChanged += OnPasswordChanged;
            confirmPassword.Content.PasswordChanged += OnConfirmPasswordChanged;
            window.registerButton.Click += OnRegisterButtonClick;
            InitSpecifiedEvents();
        }

        private void OnRegisterButtonClick(object sender, RoutedEventArgs e)
        {
            if (CheckAll())
            {
                Register();
            }

        }


        protected void CheckLogin(RegisterField<TextBox> field)
        {
            window.registerButton.IsEnabled = CheckLogin(field.Content.Text, out var message);
            field.Error.Text = message;
        }
        protected bool CheckLogin(string value, out string message)
        {
            message = "";
            if (string.IsNullOrWhiteSpace(value))
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
        protected void CheckNameLastnameSurname(RegisterField<TextBox> field)
        {
            window.registerButton.IsEnabled = CheckNameLastnameSurname(field.Content.Text, out var error);
            field.Error.Text = error;
        }
        protected bool CheckNameLastnameSurname(string value, out string message)
        {
            message = "";
            if (string.IsNullOrWhiteSpace(value))
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
            window.registerButton.IsEnabled = PasswordChecker.IsCorrectNew(password.Content.Password,
             confirmPassword.Content.Password, out var message);
            confirmPassword.Error.Text = message;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            window.registerButton.IsEnabled = PasswordChecker
                .IsCorrect(password.Content.Password, out var message);
            password.Error.Text = message;
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


    }
}
