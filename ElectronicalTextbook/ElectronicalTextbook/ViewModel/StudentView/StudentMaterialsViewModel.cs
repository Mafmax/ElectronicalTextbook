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
    public class StudentMaterialsViewModel : MaterialsViewModel
    {
        private Button leftViewButton;
        private Button rightViewButton;
        public StudentMaterialsViewModel(Window caller) : base(caller)
        {
        }

        protected override string LeftLabel =>"Необходимо пройти";

        protected override string RightLabel => "Пройденные";

        protected override IEnumerable<Button> CreateLeftButtons()
        {
            leftViewButton = new Button();
            leftViewButton.Click += OnLeftViewButtonClick;
            leftViewButton.Content = "Пройти";
            yield return leftViewButton;
        }

        private void OnLeftViewButtonClick(object sender, RoutedEventArgs e)
        {
            var material = window.leftItems.SelectedItem as Material;
            if (material is null) return;
            var viewer = new MaterialViewer(material,MaterialViewMode.View);
            throw new NotImplementedException();
        }

        protected override IEnumerable<Button> CreateRightButtons()
        {
            rightViewButton = new Button();
            rightViewButton.Click += OnRightViewButtonClick;
            rightViewButton.Content = "Посмотреть";
            yield return rightViewButton;
        }

        private void OnRightViewButtonClick(object sender, RoutedEventArgs e)
        {
            var material = window.rightItems.SelectedItem as Material;
            if (material is null) return;
            var viewer = new MaterialViewer(material,MaterialViewMode.View);
            throw new NotImplementedException();
        }
    }
}
