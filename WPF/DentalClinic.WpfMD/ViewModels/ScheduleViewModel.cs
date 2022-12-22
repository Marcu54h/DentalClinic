using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State.Navigator;

namespace DentalClinic.WpfMD.ViewModels
{
    public class ScheduleViewModel : ViewModelBase, IViewType
    {
        public ScheduleViewModel(INavigationStore navigationStore)
            : base(navigationStore)
        {

        }

        public ViewType ViewType => ViewType.ScheduleViewModel;
    }
}
