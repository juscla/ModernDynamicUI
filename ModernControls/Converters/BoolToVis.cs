namespace ModernControls.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// The bool to vis.
    /// </summary>
    public class BoolToVis : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                if (parameter.ToString() == "in")
                {
                    return !System.Convert.ToBoolean(value) ? Visibility.Visible : Visibility.Hidden;
                }
            }

            return System.Convert.ToBoolean(value) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}