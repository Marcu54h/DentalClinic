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
    public class CalendarView : ViewBase
    {
        private CalendarViewPeriodCollection _periods = new();

        public CalendarViewPeriodCollection Periods
        {
            get => _periods;
        }

        public BindingBase ItemBeginBinding { get; set; }
        public BindingBase ItemEndBinding { get; set; }

        protected override object DefaultStyleKey =>
            new ComponentResourceKey(GetType(), "DefaultStyleKey");

        protected override object ItemContainerDefaultStyleKey =>
            new ComponentResourceKey(GetType(), "ItemContainerDefaultStyleKey");
    }
}
