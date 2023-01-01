using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DentalClinic.WpfMD.Controls
{
    public class CalendarViewContentPresenter : Panel
    {
        private UIElementCollection _visualChildren;
        private bool _visualChildrenGenerated;

        protected CalendarView CalendarView => ListView.View as CalendarView;
        protected ListView ListView => TemplatedParent as ListView;

        protected override Size ArrangeOverride(Size finalSize)
        {
            int columnCount = CalendarView.Periods.Count;
            Size columnSize = new Size(finalSize.Width / columnCount,
                                       finalSize.Height);
            double elementX = 0;

            foreach (UIElement element in _visualChildren)
            {
                element.Arrange(new Rect(new Point(elementX, 0), columnSize));
                elementX += columnSize.Width;
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            GenerateVisualChildren();
            return availableSize;
        }

        private void GenerateVisualChildren()
        {
            if (_visualChildrenGenerated)
            {
                return;
            }

            if (_visualChildren is null)
            {
                _visualChildren = CreateUIElementCollection(null);
            }
            else
            {
                _visualChildren.Clear();
            }

            foreach (CalendarViewPeriod period in CalendarView.Periods)
            {
                _visualChildren.Add(new CalendarViewPeriodPresenter() { Period = period });
            }

            _visualChildrenGenerated = true;
        }
    }
}
