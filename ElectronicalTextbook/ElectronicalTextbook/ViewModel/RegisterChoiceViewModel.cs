using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicalTextbook.ViewModel
{
    public class RegisterChoiceViewModel : DialogViewModel<RegisterChoiceWindow>
    {
        public object Choice { get; private set; }
        public RegisterChoiceViewModel():base()
        {
            window.teacher.Click += OnTeacherButtonClick;
            window.student.Click += OnStudentButtonClick;
            window.exit.Click += OnExitButtonClick;
        }

        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnStudentButtonClick(object sender, RoutedEventArgs e)
        {
            Choice = new Student();
            Success();
        }


        private void OnTeacherButtonClick(object sender, RoutedEventArgs e)
        {
            Choice = new Teacher();
            Success();

        }
    }
}
