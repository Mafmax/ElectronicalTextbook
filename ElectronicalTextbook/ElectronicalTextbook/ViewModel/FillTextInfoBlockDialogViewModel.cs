using ElectronicalTextbook.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectronicalTextbook.ViewModel
{
    public class FillTextInfoBlockDialogViewModel : DialogViewModel<FillTextInfoBlockWindow>
    {
        public string Content
        {
            get
            {
                return window.content.Text;
            }
            set
            {
                window.content.Text = value;
            }
        }


        public FillTextInfoBlockDialogViewModel()
        {
            Content = "Напишите здесь текст, который отобразится в материале";
            window.confirm.Click += OnConfirmButtonClick;
        }

        private void OnConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                Content = "Здесь необходимо что-нибудь написать";
            }
            else
            {
                Success();
            }
        }
    }
}
