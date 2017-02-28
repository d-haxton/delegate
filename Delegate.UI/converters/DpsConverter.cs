using System;
using System.Globalization;
using System.Windows.Data;

namespace Delegate.UI.converters
{
    public class DpsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double valueDouble)
            {
                return $"{valueDouble:0}";
            }
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}