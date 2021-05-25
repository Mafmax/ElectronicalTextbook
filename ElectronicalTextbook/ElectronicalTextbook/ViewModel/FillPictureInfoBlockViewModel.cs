using ElectronicalTextbook.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ElectronicalTextbook.ViewModel
{
    public class FillPictureInfoBlockViewModel : DialogViewModel<FillPictureInfoBlockWindow>
    {
        public byte[] Content { get; set; }
        public FillPictureInfoBlockViewModel()
        {
            window.confirm.Click += OnConfirmButtonClick;
            window.openFile.Click += OnOpenFileClick;
            
        }

        private void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Images |*.png;*.jpg;*.jpeg;*.bmp";
            byte[] result = null;
            if (ofd.ShowDialog() == true)
            {
                string fileName = ofd.FileName;
                var bmp = new BitmapImage(new Uri(fileName));
                window.preview.Source = bmp;
                
                
                using (var stream = new MemoryStream())
                {
                    var encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bmp));
                    encoder.Save(stream);
                    result = stream.GetBuffer();
                }
                Content = result;
            }
        }


        private void OnConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            Success();
        }
    }


}
