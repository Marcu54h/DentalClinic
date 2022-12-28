using DentalClinic.Persistence;
using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State.Navigator;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WebModel;

namespace DentalClinic.WpfMD.ViewModels
{
    public class PatientsViewModel : ViewModelBase, IViewType
    {
        private IEnumerable<Patient> _patients = default!;
        private readonly IDataService<Patient> _dataService;

        public PatientsViewModel(IViewModelsFactory viewModelsFactory, INavigationStore navigationStore,
            IDataService<Patient> dataService)
            : base(navigationStore)
        {
            _dataService = dataService;

            Patients = new List<Patient>();

            var data = _dataService.GetAll().Result;

            Patients = data;

            ShowMeTheSchedule = new AsyncCommand<ViewType>(
                async (viewType) => await Task.Run(() =>
                {
                    NavigationStore.Back();
                })
            );
        }

        public IEnumerable<Patient> Patients
        {
            get => _patients;
            set => _patients = value;
        }

        public ViewType ViewType => ViewType.PatientsViewModel;

        public IAsyncCommand<ViewType> ShowMeTheSchedule { get; private set; }
    }
}
