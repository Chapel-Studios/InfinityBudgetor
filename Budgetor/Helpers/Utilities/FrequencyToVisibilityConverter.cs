using Budgetor.Models.Scheduling;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Budgetor.Helpers.Utilities
{
    public class FrequencyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var param = new List<string>((string[])parameter ?? new string[0]);
            var val = (FrequencyComboBoxItem)value;

            if (param.Contains(val?.StringValue))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
