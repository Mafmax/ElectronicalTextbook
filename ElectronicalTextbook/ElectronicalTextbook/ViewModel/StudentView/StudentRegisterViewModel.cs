using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace ElectronicalTextbook.ViewModel.StudentView
{
    public class StudentRegisterViewModel : RegisterViewModel
    {
        private RegisterField<TextBox> classNumber;
        private RegisterField<ComboBox> classSymbol;
        private ContentControl classNumberTemplate;
        private ContentControl classSymbolTemplate;
        public Student Student { get; private set; }
        public StudentRegisterViewModel(Window caller) : base(caller)
        {
            window.Title = "Регистрация ученика";
        }
        public override CalledViewModel<RegisterWindow> Init(object value)
        {
            Student = value as Student;
            return this;
        }
        protected override void InitSpecifiedEvents()
        {
            classNumber.Content.TextChanged += OnClassNumberChanged;
        }
        private void CheckClassNumber(RegisterField<TextBox> field)
        {
            window.registerButton.IsEnabled = CheckClassNumber(field.Content.Text, out string message);
            field.Error.Text = message;
        }
        private bool CheckClassNumber(string value, out string message)
        {
            message = "";
            if (string.IsNullOrWhiteSpace(value))
            {
                message = "Поле не может быть пустым";
                return false;
            }
            if (int.TryParse(value, out int res))
            {
                if (res < 1 || res > 11)
                {
                    message = "Число от 1 до 11";
                    return false;
                }
            }
            else
            {
                message = "Должно быть число";
                return false;
            }
            return true;

        }

        private void OnClassNumberChanged(object sender, TextChangedEventArgs e)
        {
            CheckClassNumber(classNumber);
        }

        protected override IEnumerable<RegisterFieldBase> SpecifiedUnpack()
        {

            classNumber = new RegisterField<TextBox>(classNumberTemplate);
            classNumber.Label.Text = "Класс: ";
            classNumber.SetChecker(x => CheckClassNumber(x.Text, out var plug));
            classSymbol = new RegisterField<ComboBox>(classSymbolTemplate);
            classSymbol.Label.Text = "Буква: ";
            classSymbol.SetChecker(x => x.SelectedItem != null);
            yield return classNumber;
            yield return classSymbol;
        }

        protected override void Register()
        {
            Student = Student ?? new Student();
            FillCommon(Student);
            DataBaseProcessor.RegisterStudent(Student,
                classNumber.Content.Text, classSymbol.Content.Text);
        }

        protected override void AddSpecified()
        {
            ControlTemplate template1 = window.FindResource("fieldTemplate") as ControlTemplate;
            ControlTemplate template2 = window.FindResource("choiceFieldTemplate") as ControlTemplate;
            classNumberTemplate = new ContentControl();
            classNumberTemplate.Template = template1;
            classSymbolTemplate = new ContentControl();
            classSymbolTemplate.Template = template2;

            window.specified.Children.Add(classNumberTemplate);
            window.specified.Children.Add(classSymbolTemplate);
        }

        protected override void SetSpecifiedStartValues()
        {
            classNumber.Content.Text = "6";
            classSymbol.Content.Items.Add("А");
            classSymbol.Content.Items.Add("Б");
            classSymbol.Content.Items.Add("В");
            classSymbol.Content.Items.Add("Г");
            classSymbol.Content.Items.Add("Д");
            classSymbol.Content.SelectedIndex = 0;
        }
    }
}
