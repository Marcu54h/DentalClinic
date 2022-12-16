namespace DentalClinic.Wpf
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool? v = (bool?)value;
                if (v == false) return Visibility.Collapsed;
                if (v == true) return Visibility.Visible;
            }
            catch (InvalidCastException)
            {
                return Visibility.Visible;
            }
            
            return Visibility.Hidden;
            //if (parameter == null)
            //    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            //else
            //    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility v = (Visibility)value;
            if (v == Visibility.Collapsed) return false;
            if (v == Visibility.Visible) return true;
            return null;
        }
    }
}
