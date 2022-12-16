namespace DentalClinic.Wpf
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    public class BooleanToStringConnectionStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                if ((string)parameter == "DBConnection")
                {
                    return "Połączenie z bazą danych OK!";
                }

                if ((string)parameter == "SMSService")
                {
                    return "Serwis SMS OK!";
                }
            }
                
            else
            {
                if ((string)parameter == "DBConnection")
                {
                    return "Brak łączności z bazą danych.";
                }

                if ((string)parameter == "SMSService")
                {
                    return "Serwis SMS nie działa, sprawdź połączenie z telefonem";
                }
            }
            return "";
                
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
