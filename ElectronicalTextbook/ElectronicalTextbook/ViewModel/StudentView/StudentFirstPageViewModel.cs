using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.View.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel.StudentView
{
    class StudentFirstPageViewModel : CalledViewModel<StudentFirstPageWindow>
    {
        public string Name { get => window.Title; set => window.Title = value; }
        public Student Student { get; private set; }
        public StudentFirstPageViewModel(Window caller) : base(caller)
        {
            window.exit.Click += OnExitButtonClick;
            window.materials.Click += OnMaterialsButtonClick;
            window.tests.Click += OnTestsButtonClick;
        }

        private void OnResultsButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnTestsButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnMaterialsButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public override CalledViewModel<StudentFirstPageWindow> Init(object value)
        {
            Student = value as Student;
            Name = $"{Student.Name} {Student.Lastname}";
            return this;
        }

    }
}
