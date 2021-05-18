using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.View.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel.TeacherView
{
    public class TeacherFirstPageViewModel : CalledViewModel<TeacherFirstPageWindow>
    {
        public string Name { get => window.Title; set => window.Title = value; }
        public Teacher Teacher { get; private set; }
        public TeacherFirstPageViewModel(Window caller) : base(caller)
        {
            window.exit.Click += OnExitButtonClick;
            window.materials.Click += OnMaterialsButtonClick;
            window.tests.Click += OnTestsButtonClick;
            window.results.Click += OnResultsButtonClick;
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

        public override CalledViewModel<TeacherFirstPageWindow> Init(object value)
        {
            Teacher = value as Teacher;
            Name = $"{Teacher.Name} {Teacher.Surname}";
            return this;
        }

    }
}
