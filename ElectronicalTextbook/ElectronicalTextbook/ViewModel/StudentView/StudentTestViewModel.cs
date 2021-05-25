using ElectronicalTextbook.Model;
using ElectronicalTextbook.Model.Controllers;
using ElectronicalTextbook.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicalTextbook.ViewModel.StudentView
{
    public class StudentTestViewModel : TestsViewModel
    {
        private Button passButton;
        private Button viewButton;
        public StudentTestViewModel(Window caller) : base(caller)
        {
        }

        protected override string LeftLabel => "Непройденные тесты";

        protected override string RightLabel => "Пройденные тесты";
        protected override IEnumerable<Button> CreateLeftButtons()
        {
            passButton = new Button();
            passButton.Click += OnPassButtonClick;
            passButton.Content = "Пройти";
            yield return passButton;
        }
        private void OnPassButtonClick(object sender, RoutedEventArgs e)
        {
            var testEstimation = window.leftItems
                .SelectedItem as TestEstimation;
            if (testEstimation is null) return;

            var viewer = new TestViewer(testEstimation, TestViewMode.Pass);
            throw new NotImplementedException();
        }

        private void OnViewButtonClick(object sender, RoutedEventArgs e)
        {
            var testEstimation = window.rightItems
                .SelectedItem as TestEstimation;
            if (testEstimation is null) return;
            var viewer = new TestViewer(testEstimation, TestViewMode.ViewErrors);
            throw new NotImplementedException();

        }

        protected override IEnumerable<Button> CreateRightButtons()
        {
            viewButton = new Button();
            viewButton.Click += OnViewButtonClick;
            viewButton.Content = "Посмотреть ошибки";
            yield return viewButton;
        }
    }
}
