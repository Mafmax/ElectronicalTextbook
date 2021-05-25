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
    public abstract class LeftRightItemsViewModel : CalledViewModel<LeftRightItemsWindow>
    {


        public LeftRightItemsViewModel(Window caller) : base(caller)
        {
        }

        private void FillLabels()
        {
            window.leftLabel.Text = LeftLabel;
            window.rightLabel.Text = RightLabel;
        }
        private void FillLists()
        {
            window.leftItems.Items.Clear();
            foreach (var item in FillLeft())
            {
                window.leftItems.Items.Add(item);
            }
            window.leftItems.SelectedIndex = 0;
            window.rightItems.Items.Clear();
            foreach (var item in FillRight())
            {
                window.rightItems.Items.Add(item);
            }
            window.rightItems.SelectedIndex = 0;
        }
        private void FillButtons()
        {
            int counter = 0;
            window.leftButtons.Children.Clear();
            window.leftButtons.ColumnDefinitions.Clear();
            foreach (var item in CreateLeftButtons())
            {
                window.leftButtons.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.SetColumn(item, counter++);
                window.leftButtons.Children.Add(item);
            }
            counter = 0;
            window.rightButtons.Children.Clear();
            window.rightButtons.ColumnDefinitions.Clear();
            foreach (var item in CreateRightButtons())
            {
                window.rightButtons.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.SetColumn(item, counter++);
                window.rightButtons.Children.Add(item);
            }
        }
        protected abstract IEnumerable<object> FillLeft();
        protected abstract IEnumerable<object> FillRight();
        protected abstract string LeftLabel { get; }
        protected abstract string RightLabel { get; }
        protected abstract IEnumerable<Button> CreateLeftButtons();
        protected abstract IEnumerable<Button> CreateRightButtons();

        protected void Fill()
        {
            FillLabels();
            FillLists();
            FillButtons();
        }
        public override void Open()
        {
            base.Open();
            Fill();
        }
    }
}
