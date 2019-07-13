using System;
using System.Globalization;
using Xamarin.Forms;

namespace WeatherApp
{
    public class CelsiusConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double f = (double)value;
            return f - 273.15;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double c = (double)value;
            return c + 273.15;
        }
    }
}
