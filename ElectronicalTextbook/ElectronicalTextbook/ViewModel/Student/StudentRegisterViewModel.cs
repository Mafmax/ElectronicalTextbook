using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using fieldType = System.ValueTuple<
    System.Windows.Controls.TextBlock,
    System.Windows.Controls.TextBox,
    System.Windows.Controls.TextBlock>;
namespace ElectronicalTextbook.ViewModel.Student
{
    public class StudentRegisterViewModel : RegisterViewModel
    {
        private fieldType classNumber;
        private fieldType classSymbol;
        private ContentControl classNumberField;
        private ContentControl classSymbolField;
        public StudentRegisterViewModel(Window caller) : base(caller)
        {
            window.Title = "Регистрация ученика";
        }

        protected override void InitSpecifiedEvents()
        {
            classNumber.Item2.TextChanged += OnClassNumberChanged;
            classSymbol.Item2.TextChanged += OnClassSymbolChanged;
        }
        private void CheckClassSymbol(fieldType pair)
        {
            window.registerButton.IsEnabled = CheckClassSymbol(pair.Item2.Text, out var message);
            pair.Item3.Text = message;
        }
        private bool CheckClassSymbol(string value, out string message)
        {
            message = "";
            if (string.IsNullOrWhiteSpace(value))
            {
                message = "Поле не может быть пустым";
                return false;
            }
            if (value.Length != 1 || !Regex.IsMatch(value, @"[аАбБвВгГдД]"))
            {
                message = "Одна буква (а/б/в/г/д)";
                return false;
            }
            return true;
        }
        private void CheckClassNumber(fieldType pair)
        {
            window.registerButton.IsEnabled = CheckClassNumber(pair.Item2.Text, out string message);
            pair.Item3.Text = message;
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
        private void OnClassSymbolChanged(object sender, TextChangedEventArgs e)
        {
            CheckClassSymbol(classSymbol);
        }

        private void OnClassNumberChanged(object sender, TextChangedEventArgs e)
        {
            CheckClassNumber(classNumber);
        }

        protected override void SpecifiedUnpack()
        {
        
            window.specified.Children.Add(classNumberField);
            window.specified.Children.Add(classSymbolField);
            var names = ("field", "value", "error");
            classNumber = UnpackTemplatedSource<TextBlock, TextBox, TextBlock>
                (names, classNumberField);
            classNumber.Item1.Text = "Класс: ";
            classSymbol = UnpackTemplatedSource<TextBlock, TextBox, TextBlock>
               (names, classSymbolField);
            classSymbol.Item1.Text = "Буква: ";
        }

        protected override void Register()
        {
            throw new NotImplementedException();
        }

        protected override void AddSpecified()
        {
            ControlTemplate template = window.FindResource("fieldContainer") as ControlTemplate;
            classNumberField = new ContentControl();
            classNumberField.Template = template;
            classSymbolField = new ContentControl();
            classSymbolField.Template = template;
        }
    }
}
