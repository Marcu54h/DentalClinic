using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;

namespace DentalClinic.WpfMD.ViewModels
{
    public class ScheduleViewModel : ViewModelBase, IViewType
    {
        public ViewType ViewType => ViewType.ScheduleViewModel;
    }
}
