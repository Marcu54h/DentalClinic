using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Unity;

namespace DentalClinic.Wpf
{
    public class TreatmentsCountToColorConverter : IValueConverter
    {
        private GuiSettings appSettings = App.container.Resolve<Container>().GuiSettings;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush unfilledVisitsColor = new SolidColorBrush(appSettings.UnfilledVisitsColor);
            return (bool)value == true ? new SolidColorBrush(Colors.Black) : unfilledVisitsColor;
            //return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
