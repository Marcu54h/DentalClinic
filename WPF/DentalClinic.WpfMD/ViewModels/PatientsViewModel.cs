using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State.Navigator;

namespace DentalClinic.WpfMD.ViewModels
{
    public class PatientsViewModel : ViewModelBase, IViewType
    {
        public PatientsViewModel()
        {

        }

        public ViewType ViewType => ViewType.PatientsViewModel;
    }
}
