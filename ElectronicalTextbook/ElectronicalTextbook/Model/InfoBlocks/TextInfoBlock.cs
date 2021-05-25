using ElectronicalTextbook.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectronicalTextbook.Model.InfoBlocks
{
    public class TextInfoBlock : InfoBlock
    {
   
        private TextBlock tb;

        public override Panel Draw()
        {
            tb = new TextBlock();
            tb.Text = Content;
            Grid grid = new Grid();
            grid.Children.Add(tb);
            return grid;
        }

        public override void Fill()
        {
            var dlg = new FillTextInfoBlockDialogViewModel();
            if (dlg.ShowAsDialog() == true)
            {
                Content = dlg.Content;
            }
            else
            {
                Content = "Empty or error";
            }
        }
        public override string ToString()
        {
            return "Блок текста";
        }

    }
}
