using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ElectronicalTextbook.Model.Controllers
{
    public class MaterialViewer : Viewer
    {
        Button saveButton;
        private readonly Material material;
        private readonly InfoBlockProvider provider;
        private readonly MaterialViewMode viewMode;
        private StackPanel content;
        private List<InfoBlock> blocks = new List<InfoBlock>();
        private List<Panel> blockContainers = new List<Panel>();
        public MaterialViewer(Material material, MaterialViewMode mode = MaterialViewMode.View, InfoBlockProvider provider = null)
        {
            this.material = material;
            this.provider = provider ?? new MaterialsInfoBlockProvider();
            this.viewMode = mode;
        }
        private IEnumerable<Student> GetTargetStudents(string formattedString)
        {
            string[] targets = formattedString.Split(';');
            List<Student> students = new List<Student>();
            for (int i = 0; i < targets.Length; i++)
            {
                IEnumerable<Student> studentsIterator =null; 
                if(int.TryParse(targets[i], out int classNumber))
                {
                    studentsIterator = DataBaseProcessor.GetStudents(classNumber);
                }
                else
                {
                    var number = int.Parse(Regex.Match(targets[i], @"[0-9]").ToString());
                    var symbol = Regex.Match(targets[i], @"[а-д]").ToString();
                    studentsIterator = DataBaseProcessor.GetStudents(number, symbol);
                }
                foreach (var item in studentsIterator)
                {
                    if (!students.Contains(item))
                    {
                        students.Add(item);
                        yield return item;
                    }
                }
            }
        }
        protected override void SaveChanges()
        {
            if (blocks.Count == 0) return;
            
            DataBaseProcessor.UploadMaterial(material);

        }
        public override Panel View()
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(
                new RowDefinition() { Height = new GridLength(30, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(
                new RowDefinition() { Height = new GridLength(40) });
            ScrollViewer viewer = new ScrollViewer();
            if (viewMode == MaterialViewMode.Edit)
            {
                Menu menu = new Menu();
                MenuItem add = new MenuItem() { Header = "Добавить элемент" };
                menu.Items.Add(add);
                foreach (var item in provider.ShowAvaliable())
                {
                    MenuItem block = new MenuItem() { Header = item.ToString(), DataContext = item };
                    block.Click += OnMenuItemSelected;
                    add.Items.Add(block);
                }
                Grid.SetRow(menu, 0);
                Grid.SetRow(viewer, 1);
                grid.Children.Add(menu);
            }
            else
            {
                Grid.SetRow(viewer, 0);
                Grid.SetRowSpan(viewer, 2);
            }
            content = new StackPanel();
            viewer.Content = content;
            saveButton = new Button();
            foreach (var item in material.Content)
            {
                AddInfoBlock(item);
            }

            Grid.SetRow(saveButton, 2);
            saveButton.Content = GetButtonContent();
            saveButton.Click += OnSaveButtonClick;
            grid.Children.Add(viewer);
            grid.Children.Add(saveButton);
            return grid;
        }
        private string GetButtonContent()
        {
            if (viewMode == MaterialViewMode.View)
            {
                return "Закончить просмотр";
            }
            else
            {
                if(blocks.Count == 0)
                {
                    return "Отменить";
                }
                else
                {
                    return "Сохранить и выйти";
                }
            }
        }
        private void AddInfoBlock(InfoBlock block)
        {
            var temp = FillBlock(block);
            blockContainers.Add(temp);
            content.Children.Add(temp);
            content.UpdateLayout();
            blocks.Add(block);
            saveButton.Content = GetButtonContent();
        }
        private void OnMenuItemSelected(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            var block = item.DataContext as InfoBlock;
            block.Fill();
            AddInfoBlock(provider.GetInstance(block));
        }
        private Image CreateImage(string source)
        {
            var path = Environment.CurrentDirectory
                .Replace("\\bin\\Debug", "") + "\\Resources\\Images\\";
            var bmp = new BitmapImage(new Uri(path + source));
            Image img = new Image();
            img.Source = bmp;
            return img;
        }
        private Panel FillBlock(InfoBlock context)
        {
            if (viewMode == MaterialViewMode.View)
            {
                return context.Draw();
            }
            else
            {
                Grid mainGrid = new Grid();
                Border frame = new Border();
                frame.BorderBrush = Brushes.Black;
                frame.BorderThickness = new Thickness(1);
                mainGrid.Margin = new Thickness(2, 0, 2, 5);
                Grid grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
                var material = context.Draw();
                grid.Background = Brushes.LightGray;
                Grid.SetRowSpan(material, 3);
                Button up = new Button();
                up.DataContext = context;
                up.Click += OnUpButtonClick;
                up.Content = CreateImage("Arrow up.png");
                Button down = new Button();
                down.DataContext = context;
                down.Click += OnDownButtonClick;
                down.Content = CreateImage("Arrow down.png");
                Button delete = new Button();
                delete.DataContext = context;
                delete.Click += OnDeleteButtonClick;
                delete.Content = CreateImage("Delete.png");
                Grid.SetColumn(up, 1);
                Grid.SetColumn(delete, 1);
                Grid.SetColumn(down, 1);
                Grid.SetRow(up, 0);
                Grid.SetRow(delete, 1);
                Grid.SetRow(down, 2);
                grid.Children.Add(up);
                grid.Children.Add(delete);
                grid.Children.Add(down);
                mainGrid.Children.Add(grid);
                grid.Children.Add(material);
                mainGrid.Children.Add(frame);
                return mainGrid;
            }
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var block = button.DataContext as InfoBlock;
            int currentIndex = blocks.IndexOf(block);
            content.Children.RemoveAt(currentIndex);
            blocks.RemoveAt(currentIndex);
            blockContainers.RemoveAt(currentIndex);
            saveButton.Content = GetButtonContent();
        }

        private void OnDownButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var block = button.DataContext as InfoBlock;
            int currentIndex = blocks.IndexOf(block);
            if (currentIndex == blocks.Count - 1)
            {
                return;
            }
            ReplaceBlocks(currentIndex, currentIndex + 1);

        }
        private void ReplaceBlocks(int index1, int index2)
        {
            content.Children.Clear();
            int start = index1 > index2 ? index2 : index1;
            blockContainers.Reverse(start, 2);
            blocks.Reverse(start, 2);
            foreach (var item in blockContainers)
            {
                content.Children.Add(item);
            }
            content.UpdateLayout();
        }
        private void OnUpButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var block = button.DataContext as InfoBlock;
            int currentIndex = blocks.IndexOf(block);
            if (currentIndex == 0)
            {
                return;
            }
            ReplaceBlocks(currentIndex, currentIndex - 1);
        }
     
        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (viewMode == MaterialViewMode.View)
            {
                Close(false);
                return;
            }
            var dlg = new ConfigureMaterialDialog();
            material.Content = blocks;
            dlg.Theme = material.Theme ?? "Тема";
            if (dlg.ShowAsDialog() == true)
            {
                material.Theme = dlg.Theme;
                material.StudentsWhoNotComplete =
                    new List<Student>(GetTargetStudents(dlg.Target));
                material.Speciality = DataBaseProcessor
                    .GetSpecialities()
                    .First(x => x.Name.Equals(dlg.Speciality));
            }
            else
            {
                return;
            }
            Close(true);
        }
    }
}
