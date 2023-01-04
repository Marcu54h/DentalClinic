using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalClinic.Persistence;
using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State.Navigator;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebModel;

namespace DentalClinic.WpfMD.ViewModels
{
    public partial class PatientsViewModel : ObservableRecipient, IViewType
    {
        [ObservableProperty]
        private IEnumerable<Patient> patients = default!;
        private readonly IDataService<Patient> _dataService;
        private readonly IViewModelsFactory _viewModelFactory;
        private readonly INavigationStore _navigationStore;

        public PatientsViewModel(IViewModelsFactory viewModelsFactory, INavigationStore navigationStore,
            IDataService<Patient> dataService)
        {
            _dataService = dataService;
            _viewModelFactory = viewModelsFactory;
            _navigationStore = navigationStore;

            Patients = new List<Patient>();

            Patients = Task.Run(() => _dataService.GetAll(p => p.Person, pers => pers.Addresses)).Result;
        }

        [RelayCommand]
        private void ShowShedule(ViewType viewType)
        {
            _navigationStore.CurrentView = _viewModelFactory.Create(viewType);
        }

        public ViewType ViewType => ViewType.PatientsView;
    }
}
