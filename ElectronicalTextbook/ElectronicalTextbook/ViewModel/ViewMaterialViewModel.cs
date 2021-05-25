using ElectronicalTextbook.Model.Controllers;
using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel
{
    public class ViewMaterialViewModel : CalledViewModel<EmptyWindow>
    {
        private  MaterialViewer viewer;
        public ViewMaterialViewModel(Window caller) : base(caller)
        {
        }
        public override CalledViewModel<EmptyWindow> Init(object value)
        {
            viewer = value as MaterialViewer;
            return this;
        }
        public override void Open()
        {
            window.mainGrid.Children.Add(viewer.View());
            viewer.OnClose += OnFinishedView;
            base.Open();
        }

        private void OnFinishedView(object sender, EventArgs e)
        {
            Close();
        }
    }
}
