using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using System.Threading.Tasks;

namespace DentalClinic.WpfMD.ViewModels
{
    public class MainViewModel : ViewModelBase, IViewType
    {
        public ViewType ViewType => ViewType.MainViewModel;
        private bool isLeftDrawerOpen = false;
        private IViewType currentView = default!;
        private IViewModelsFactory _viewModelsFactory;
        

        public MainViewModel(IViewModelsFactory viewModelsFactory)
        {
            _viewModelsFactory = viewModelsFactory;
            CurrentView = new PatientsViewModel();

            CommandToOpenLeftDrawer = new AsyncCommand<bool>(
                async (choice) => await Task.Run(() => IsLeftDrawerOpen = choice));

            CommandToChangeView = new AsyncCommand<ViewType>(
                async (viewType) => await Task.Run(() =>
                {
                    CurrentView = _viewModelsFactory.Create(viewType);
                    IsLeftDrawerOpen = false;
                })
            );
        }

        public IViewType CurrentView
        {
            get => currentView;
            set
            {
                if (currentView != value) { currentView = value; }
                Changed(nameof(CurrentView));
            }
        }

        public bool IsLeftDrawerOpen
        {
            get => isLeftDrawerOpen;
            set
            {
                isLeftDrawerOpen = value;
                Changed(nameof(IsLeftDrawerOpen));
            }
        }

        public IAsyncCommand<bool> CommandToOpenLeftDrawer { get; set; }
        public IAsyncCommand<ViewType> CommandToChangeView { get; set; }
    }
}
