using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State.Navigator;
using System.Threading.Tasks;

namespace DentalClinic.WpfMD.ViewModels
{
    public class PatientsViewModel : ViewModelBase, IViewType
    {
        private readonly IViewModelsFactory _viewModelsFactory;

        public PatientsViewModel(IViewModelsFactory viewModelsFactory, INavigationStore navigationStore)
            : base(navigationStore)
        {
            _viewModelsFactory = viewModelsFactory;

            ShowMeTheSchedule = new AsyncCommand<ViewType>(
                async (viewType) => await Task.Run(() =>
                {
                    NavigationStore.CurrentView = _viewModelsFactory.Create(viewType);
                })
            );
        }

        public ViewType ViewType => ViewType.PatientsViewModel;

        public IAsyncCommand<ViewType> ShowMeTheSchedule { get; private set; }

        
    }
}
