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

namespace ElectronicalTextbook.ViewModel
{
    public class ConfigureMaterialDialog : DialogViewModel<ConfigureMaterialWindow>
    {
        public string Speciality
        {
            get
            {
                return window.specialities.SelectedItem.ToString();
            }
            set
            {
                window.specialities.SelectedItem = value;
            }
        }
        public string Target
        {
            get
            {
                return window.studentsBox.Text;
            }
            set
            {
                window.studentsBox.Text = value;
            }
        }
        public string Theme
        {
            get
            {
                return window.themeBox.Text;
            }
            set
            {
                window.themeBox.Text = value;
            }
        }


        public ConfigureMaterialDialog()
        {


            if (string.IsNullOrWhiteSpace(Theme))
            {
                Theme = "Тема материала";
            }
            foreach (var item in DataBaseProcessor.GetSpecialities())
            {
                window.specialities.Items.Add(item);
            }
            window.specialities.SelectedIndex = 0;
            window.confirmBtn.Click += OnConfirmButtonClick;
        }
        private bool CheckTarget(string target, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(target))
            {
                error = "Целевая аудитория не может быть пустой";
                return false;
            }
            string[] targets = target.Split(';');
            for (int i = 0; i < targets.Length; i++)
            {
                if (!Regex.IsMatch(targets[i], @"[0-9а-д]"))
                {
                    error = "Целевая аудитория не соответствует шаблону";
                    return false;
                }
            }
            return true;
        }
        private void OnConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (!CheckTarget(Target, out var message))
            {
                window.errorBox.Text = message;
            }
            else if (DataBaseProcessor.IsExistMaterial(Theme))
            {
                window.errorBox.Text = "Материал с такой темой уже существует";
            }
            else
            {
                Success();

            }
        }
    }
}
