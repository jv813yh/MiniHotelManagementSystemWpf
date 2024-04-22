using System.Windows;
using System.Windows.Data;

namespace HotelReservationsWpf.Converters
{
    public class InverseToBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Collapsed: Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        // ConvertBack is not implemented for a OneWay binding
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
