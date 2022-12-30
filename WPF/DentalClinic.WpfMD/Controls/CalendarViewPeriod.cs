using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DentalClinic.WpfMD.Controls
{
    public class CalendarViewPeriod : DependencyObject
    {
        public static readonly DependencyProperty BeginProperty =
            DependencyProperty.Register("Begin", typeof(DateTime),
                typeof(CalendarViewPeriod));

        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(DateTime),
                typeof(CalendarViewPeriod));

        public static readonly DependencyProperty HeaderProperty = 
            DependencyProperty.Register("Header", typeof(object),
                typeof(CalendarViewPeriod));

        public DateTime Begin
        {
            get => (DateTime)GetValue(BeginProperty);
            set => SetValue(BeginProperty, value);
        }

        public DateTime End
        {
            get => (DateTime)GetValue(EndProperty);
            set => SetValue(EndProperty, value);
        }

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
    }
}
