using System;
using System.Globalization;
using System.Windows.Data;

namespace se2_loon_hh.Forms.FormsAPI
{
    class RadioBoolToLongConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (long)value == long.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }

    }
}
