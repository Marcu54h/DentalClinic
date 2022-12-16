namespace DentalClinic.Wpf
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;
    using Unity;

    public class MonthDayToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((DateTime)value).Year == DateTime.Today.Year &&
                ((DateTime)value).Month == DateTime.Today.Month &&
                ((DateTime)value).Day == DateTime.Today.Day)
                return new SolidColorBrush(Colors.LightGray);

            if (((DateTime)value).DayOfWeek == DayOfWeek.Saturday)
                return new SolidColorBrush(Color.FromRgb(72, 203, 198));
            if (((DateTime)value).DayOfWeek == DayOfWeek.Sunday)
                return new SolidColorBrush(Color.FromRgb(72, 224, 206));
            return new SolidColorBrush(Colors.Transparent);
            //if (parameter == null)
            //    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            //else
            //    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
