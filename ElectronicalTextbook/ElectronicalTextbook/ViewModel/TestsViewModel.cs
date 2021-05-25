using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel
{
    public abstract class TestsViewModel : LeftRightItemsViewModel
    {
        protected User User { get; set; }
        protected TestsViewModel(Window caller) : base(caller)
        {
        }
        protected sealed override IEnumerable<object> FillLeft()
        {
            return DataBaseProcessor.GetInterestingTests(User);
        }
        protected sealed override IEnumerable<object> FillRight()
        {
            return DataBaseProcessor.GetOtherTests(User);
        }
        public override CalledViewModel<LeftRightItemsWindow> Init(object value)
        {
            User = value as User;
            return base.Init(value);
        }
    }
}
