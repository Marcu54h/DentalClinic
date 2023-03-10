namespace DentalClinic.Wpf
{

    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// 
    /// </summary>
    [ValueConversion(typeof(ItemsPresenter), typeof(Orientation))]
    public class HideIfEmptyConverter : IValueConverter
    {
        // Returns 'Horizontal' for root TreeViewItems 
        // and 'Vertical' for all other items.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // The 'value' argument should reference 
            // an ItemsPresenter.
            ItemsPresenter itemsPresenter = value as ItemsPresenter;
            if (itemsPresenter == null)
                return Binding.DoNothing;

            // The ItemsPresenter's templated parent 
            // should be a TreeViewItem.
            TreeViewItem item = itemsPresenter.TemplatedParent as TreeViewItem;
            if (item == null)
                return Binding.DoNothing;

            // If the item is contained in a TreeView then it is
            // a root item.  Otherwise it is contained in another
            // TreeViewItem, in which case it is not a root.
            bool isRoot = ItemsControl.ItemsControlFromItemContainer(item) is TreeView;

            if (isRoot)
            {
                return item.HasItems ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; 
            }
            return System.Windows.Visibility.Visible;
        }

        public object ConvertBack(
         object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back.");
        }
    }
}
