using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicalTextbook.ViewModel.Teacher
{
    public class TeacherRegisterViewModel : RegisterViewModel
    {

        private RegisterField<ComboBox> speciality;
        private ContentControl specialityTemplate;

        public TeacherRegisterViewModel(Window caller) : base(caller)
        {
        }

        protected override void AddSpecified()
        {
            var template = window.FindResource("choiceFieldTemplate") as ControlTemplate;
            specialityTemplate = new ContentControl() { Template = template };
            window.specified.Children.Add(specialityTemplate);


        }

        protected override void InitSpecifiedEvents()
        {
            return;
        }

        protected override void Register()
        {
            throw new NotImplementedException();
        }

        protected override void SetSpecifiedStartValues()
        {
            string[] specialities = DataBaseProcessor.GetSpecialities();

            for (int i = 0; i < specialities.Length; i++)
            {
                speciality.Content.Items.Add(specialities[i]);
            }
            speciality.Content.SelectedIndex = 0;
        }

        protected override IEnumerable<RegisterFieldBase> SpecifiedUnpack()
        {
            speciality = new RegisterField<ComboBox>(specialityTemplate);
            speciality.Label.Text = "Специальность";
            speciality.SetChecker(x => x.SelectedItem != null);
            yield return speciality;
        }
    }
}
