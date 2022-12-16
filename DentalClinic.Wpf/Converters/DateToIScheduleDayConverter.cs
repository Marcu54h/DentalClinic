namespace DentalClinic.Wpf
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Unity;

    public class DateToIScheduleDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is null))
            {
                IScheduleDay scheduleDay = (IScheduleDay)value;
                return scheduleDay.Date;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            IScheduleDay scheduleDay = App.container.Resolve<IScheduleDay>();
            scheduleDay.Date = dateTime;

            return scheduleDay;
        }
    }
}
