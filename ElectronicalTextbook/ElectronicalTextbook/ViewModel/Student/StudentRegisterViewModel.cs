using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace ElectronicalTextbook.ViewModel.Student
{
    public class StudentRegisterViewModel : RegisterViewModel
    {
        private RegisterField<TextBox> classNumber;
        private RegisterField<ComboBox> classSymbol;
        private ContentControl classNumberField;
        private ContentControl classSymbolField;
        public StudentRegisterViewModel(Window caller) : base(caller)
        {
            window.Title = "Регистрация ученика";
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

            window.specified.Children.Add(classNumberField);
            window.specified.Children.Add(classSymbolField);
            classNumber = new RegisterField<TextBox>(classNumberField);
            classNumber.Label.Text = "Класс: ";
            classNumber.SetChecker(x => CheckClassNumber(x.Text, out var plug));
            classSymbol = new RegisterField<ComboBox>(classSymbolField);
            classSymbol.Content.Text = "Буква: ";
            classSymbol.SetChecker(x => x.SelectedItem != null);
            yield return classNumber;
            yield return classSymbol;
        }

        protected override void Register()
        {
            throw new NotImplementedException();
        }

        protected override void AddSpecified()
        {
            ControlTemplate template1 = window.FindResource("fieldContainer") as ControlTemplate;
            ControlTemplate template2= window.FindResource("choiceFieldContainer") as ControlTemplate;
            classNumberField = new ContentControl();
            classNumberField.Template = template1;
            classSymbolField = new ContentControl();
            classSymbolField.Template = template2;
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
