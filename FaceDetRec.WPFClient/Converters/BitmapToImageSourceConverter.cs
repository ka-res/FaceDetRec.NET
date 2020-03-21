using System;
using System.Drawing;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FaceDetRec.WPFClient.Converters
{
    public class BitmapToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            using (var ms = new MemoryStream())
            {
                //todo: obsluga wywalania sie GDI
                ((Bitmap) value).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                var image = new BitmapImage();
                image.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = new MemoryStream(ms.ToArray());
                image.EndInit();

                return image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
