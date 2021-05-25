using ElectronicalTextbook.Model.DataBase;
using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicalTextbook.ViewModel
{
    public abstract class MaterialsViewModel : LeftRightItemsViewModel
    {
        public User User { get; protected set; }
        protected MaterialsViewModel(Window caller) : base(caller)
        {
        }

        public override CalledViewModel<LeftRightItemsWindow> Init(object value)
        {
            User = value as User;
            return base.Init(value);
        }
        protected override IEnumerable<object> FillLeft()
        {
            return DataBaseProcessor.GetInterestingMaterials(User);
        }

        protected override IEnumerable<object> FillRight()
        {
            return DataBaseProcessor.GetOtherMaterials(User);
        }
    }
}
