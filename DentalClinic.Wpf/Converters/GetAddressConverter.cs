namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    public class GetAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Person p = value as Person;

            try
            {
                Address address = MainDataContext.MainContext.Addresses.Where(x => x.PersonId == p.Id).FirstOrDefault();

                if (!(address is null))
                {
                    return address.CellPhone;
                }
                
            }
            catch
            {
                MessageBox.Show("Błąd podczas pobierania informacji o adresach osoby.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
