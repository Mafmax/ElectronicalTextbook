using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicalTextbook.ViewModel.TeacherView
{
    public class TeacherTestsViewModel : TestsViewModel
    {
        public TeacherTestsViewModel(Window caller) : base(caller)
        {
        }

        protected override string LeftLabel => "Ваши тесты";

        protected override string RightLabel => "Тесты других учителей";

        protected override IEnumerable<Button> CreateLeftButtons()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Button> CreateRightButtons()
        {
            throw new NotImplementedException();
        }
    }
}
