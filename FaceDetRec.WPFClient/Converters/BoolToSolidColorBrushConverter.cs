using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FaceDetRec.WPFClient.Converters
{
    public class BoolToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (bool) value ? new SolidColorBrush(Colors.DarkRed) : new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
