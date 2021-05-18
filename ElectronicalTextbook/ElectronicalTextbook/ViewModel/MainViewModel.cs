using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.ViewModel.AdminView;
using ElectronicalTextbook.ViewModel.StudentView;
using ElectronicalTextbook.ViewModel.TeacherView;
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
        private void Enter(User user)
        {
            if (user is Admin admin)
            {
                new AdminFirstPageViewModel(Window)
                    .Init(admin)
                    .Open();
            }
            if(user is Teacher teacher)
            {
                new TeacherFirstPageViewModel(Window)
                    .Init(teacher)
                    .Open();
            }
            if(user is Student student)
            {
                new StudentFirstPageViewModel(Window)
                    .Init(student)
                    .Open();
            }
            Window.passwordTxt.Password = "";
        }
        private void TryEnter(string username, string password)
        {


            bool correct = DataBaseProcessor.TryEnter(username, password, out var user);
            Window.errorText.Visibility = Visibility.Hidden;
            if (correct) Enter(user);
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
            var dlg = new AdminPasswordDialogViewModel();
            if (dlg.ShowAsDialog() == true)
            {
                var adminPage = new AdminFirstPageViewModel(Window);
                adminPage.Open();
            }
        }
        private void OnPasswordRecoveryButtonClick(object sender, RoutedEventArgs e)
        {
            var changePassword = new ChangePasswordViewModel(Window)
                .Init(Window.loginTxt.Text);
            changePassword.SetCallback(OnPasswordChanged);
            changePassword.Open();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var changer = sender as ChangePasswordViewModel;
            Window.loginTxt.Text = changer.User;
        }

        private void OnRegisterButtonClick(object sender, RoutedEventArgs e)
        {
            var regDlg = new RegisterChoiceViewModel();
            if (regDlg.ShowAsDialog() == true)
            {
                User choice = regDlg.Choice as User;
                if (choice is Student student)
                {

                    new StudentRegisterViewModel(Window)
                    .Init(student)
                    .SetCallback(OnRegister)
                    .Open();
                }
                if (choice is Teacher teacher)
                {
                    new TeacherRegisterViewModel(Window)
                    .Init(teacher)
                    .SetCallback(OnRegister)
                    .Open();

                }


            }
        }

        private void OnRegister(object sender, RoutedEventArgs e)
        {
            if (sender is TeacherRegisterViewModel teacherReg)
            {
                Window.loginTxt.Text = teacherReg.Teacher.Username;
            }
            if (sender is StudentRegisterViewModel studentReg)
            {
                Window.loginTxt.Text = studentReg.Student.Username;
            }
        }
    }
}
