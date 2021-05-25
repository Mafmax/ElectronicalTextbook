using ElectronicalTextbook.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ElectronicalTextbook.Model.InfoBlocks
{
    public class PictureInfoBlock : InfoBlock
    {

        public override Panel Draw()
        {
            byte[] content = null;
            try
            {

                content = Convert.FromBase64String(Content);
            }
            catch (Exception ex)
            {
                return new Grid();
            }
            Grid grid = new Grid();
            Image image = new Image();
            image.Source = LoadImage(content);
            grid.Children.Add(image);

            return grid;

        }
        private static BitmapSource LoadImage(byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }
        public override void Fill()
        {
            var dialog = new FillPictureInfoBlockViewModel();
            if (dialog.ShowAsDialog() == true)
            {
                Content = Convert.ToBase64String(dialog.Content);
            }
        }
        public override string ToString()
        {
            return "Блок картинки";
        }
    }
}
