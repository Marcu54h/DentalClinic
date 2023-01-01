using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DentalClinic.WpfMD.Controls
{
    [TemplatePart(Name = "PART_Header", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_TimeScale", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(TextBlock))]
    public class CalendarView : ViewBase
    {
        #region Static
        public static DependencyProperty BeginProperty =
            DependencyProperty.RegisterAttached("Begin", typeof(DateTime),
                typeof(ListViewItem));
        public static DateTime GetBegin(DependencyObject item) =>
            (DateTime)item.GetValue(BeginProperty);

        public static void SetBegin(DependencyObject item, DateTime value) =>
            item.SetValue(BeginProperty, value);


        public static DependencyProperty EndProperty =
            DependencyProperty.RegisterAttached("End", typeof(DateTime),
                typeof(ListViewItem));

        public static DateTime GetEnd(DependencyObject item) =>
            (DateTime)item.GetValue(EndProperty);

        public static void SetEnd(DependencyObject item, DateTime value) =>
            item.SetValue(EndProperty, value);

        #endregion // Static


        private CalendarViewPeriodCollection _periods = new();
        public CalendarViewPeriodCollection Periods => _periods;

        public BindingBase ItemBeginBinding { get; set; } = default!;
        public BindingBase ItemEndBinding { get; set; } = default!;

        public bool PeriodContainsItem(ListViewItem item, CalendarViewPeriod period)
        {
            DateTime itemBegin = (DateTime)item.GetValue(BeginProperty);
            DateTime itemEnd = (DateTime)item.GetValue(EndProperty);

            return ((itemBegin <= period.Begin) && (itemEnd >= period.Begin)) ||
                   ((itemBegin <= period.End)   && (itemEnd >= period.Begin));
        }


        #region Overrides
        protected override object DefaultStyleKey =>
            new ComponentResourceKey(GetType(), "DefaultStyleKey");

        protected override object ItemContainerDefaultStyleKey =>
            new ComponentResourceKey(GetType(), "ItemContainerDefaultStyleKey");

        protected override void PrepareItem(ListViewItem item)
        {
            item.SetBinding(BeginProperty, ItemBeginBinding);
            item.SetBinding(EndProperty, ItemEndBinding);
        }
        #endregion // Overrides

    }
}
