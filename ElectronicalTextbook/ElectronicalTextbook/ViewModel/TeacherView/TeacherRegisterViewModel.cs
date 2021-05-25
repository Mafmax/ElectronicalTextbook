using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.View;

namespace ElectronicalTextbook.ViewModel.TeacherView
{
    public class TeacherRegisterViewModel : RegisterViewModel
    {

        private RegisterField<ComboBox> speciality;
        private ContentControl specialityTemplate;
        public Teacher Teacher { get; private set; }

        public TeacherRegisterViewModel(Window caller) : base(caller)
        {
        }
        public override CalledViewModel<RegisterWindow> Init(object value)
        {
            Teacher = value as Teacher;
            return this;
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

            Teacher = Teacher ?? new Teacher();
            FillCommon(Teacher);
            DataBaseProcessor.RegisterTeacher(Teacher, speciality.Content.Text);
        }

        protected override void SetSpecifiedStartValues()
        {
            foreach (var item in DataBaseProcessor.GetSpecialities())
            {
                speciality.Content.Items.Add(item);
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
