namespace DentalClinic.Wpf
{

    using DentalClinic.Windows;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Unity;

    public class PeselToFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IPesel pesel = App.container.Resolve<IPesel>();

            if (pesel.IsMale((string)value) == true)
                return "pack://application:,,,/Resources/Images/default_avatar_male.png";
            if (pesel.IsMale((string)value) == false)
                return "pack://application:,,,/Resources/Images/default_avatar_female.png";
            return "pack://application:,,,/Resources/Images/default_avatar_male.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
