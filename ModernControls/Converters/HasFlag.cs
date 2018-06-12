namespace ModernControls.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using ModernControls.Controls;

    /// <summary>
    /// The has flag.
    /// </summary>
    public class HasFlag : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return Visibility.Collapsed;
            }

            WindowButtons result;
            if (!Enum.TryParse(value.ToString(), true, out result))
            {
                return Visibility.Collapsed;
            }

            WindowButtons param;
            if (Enum.TryParse(parameter.ToString(), true, out param))
            {
                return result.HasFlag(param) ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}