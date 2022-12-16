namespace DentalClinic.Wpf
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class SortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                IList collection = (IList)value;

                ListCollectionView view = new ListCollectionView(collection);

                SortDescription sort = new SortDescription(parameter.ToString(), ListSortDirection.Ascending);

                view.SortDescriptions.Add(sort);

                return view;
            }
            catch
            {
                return value;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
