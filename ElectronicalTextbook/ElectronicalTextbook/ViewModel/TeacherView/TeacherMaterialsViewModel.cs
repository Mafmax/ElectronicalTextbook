using ElectronicalTextbook.Model.Controllers;
using ElectronicalTextbook.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicalTextbook.ViewModel.TeacherView
{
    public class TeacherMaterialsViewModel : MaterialsViewModel
    {
        private Button addButton;
        private Button viewOrChangeButton;
        private Button viewButton;
        public TeacherMaterialsViewModel(Window caller) : base(caller)
        {
        }

        protected override string LeftLabel => "Ваши материалы";
        protected override string RightLabel => "Чужие материалы";

        protected override IEnumerable<Button> CreateLeftButtons()
        {
            addButton = new Button();
            addButton.Click += OnAddButtonClick;
            addButton.Content = "Добавить";
            yield return addButton;
            viewOrChangeButton = new Button();
            viewOrChangeButton.Click += OnViewOrChangeButtonClick;
            viewOrChangeButton.Content = "Посмотреть/изменить";
            yield return viewOrChangeButton;
        }

        private void OnViewOrChangeButtonClick(object sender, RoutedEventArgs e)
        {
            var material = window.leftItems.SelectedItem as Material;
            if (material is null) return;
            var viewer = new MaterialViewer(material,  MaterialViewMode.Edit);
            OpenViewer(viewer);
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            var material = new Material();
            var teacher = User as Teacher;
            material.Teacher = teacher; 
            var viewer = new MaterialViewer(material, MaterialViewMode.Edit);

            OpenViewer(viewer);
        }

        
        protected override IEnumerable<Button> CreateRightButtons()
        {
            viewButton = new Button();
            viewButton.Click += OnViewButtonClick;
            viewButton.Content = "Посмотреть";
            yield return viewButton;
        }
        
        private void OnViewButtonClick(object sender, RoutedEventArgs e)
        {
            var material = window.rightItems.SelectedItem as Material;
            if (material is null) return;
            var viewer = new MaterialViewer(material,MaterialViewMode.View);
            OpenViewer(viewer);
        }
    }
}
