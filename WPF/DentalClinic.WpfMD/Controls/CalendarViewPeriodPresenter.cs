using System.Windows;
using System.Windows.Controls;

namespace DentalClinic.WpfMD.Controls
{
    public class CalendarViewPeriodPresenter : Control
    {
        static CalendarViewPeriodPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarViewPeriodPresenter),
                new FrameworkPropertyMetadata(typeof(CalendarViewPeriodPresenter)));
        }

        public CalendarViewPeriod Period { get; set; }
    }
}