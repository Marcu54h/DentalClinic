using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State.Navigator;
using System.Threading.Tasks;

namespace DentalClinic.WpfMD.ViewModels
{
    public class MainViewModel : ViewModelBase, IViewType
    {
        public ViewType ViewType => ViewType.MainViewModel;
        private bool isLeftDrawerOpen = false;
        private readonly IViewModelsFactory _viewModelsFactory;        

        public MainViewModel(IViewModelsFactory viewModelsFactory, INavigationStore navigationStore)
            : base(navigationStore)

        {
            _viewModelsFactory = viewModelsFactory;
            CurrentView = this.NavigationStore.CurrentView;

            CommandToOpenLeftDrawer = new AsyncCommand<bool>(
                async (choice) => await Task.Run(() => IsLeftDrawerOpen = choice));

            CommandToChangeView = new AsyncCommand<ViewType>(
                async (viewType) => await Task.Run(() =>
                {
                    NavigationStore.CurrentView = _viewModelsFactory.Create(viewType);
                    IsLeftDrawerOpen = false;
                })
            );
            NavigationStore.CurrentViewChanged += OnCurrentViewChanged;
        }

        public bool IsLeftDrawerOpen
        {
            get => isLeftDrawerOpen;
            set => SetField(ref isLeftDrawerOpen, value);
        }

        public IAsyncCommand<bool> CommandToOpenLeftDrawer { get; private set; }
        public IAsyncCommand<ViewType> CommandToChangeView { get; private set; }

        private void OnCurrentViewChanged(IViewType view) => CurrentView = view;
        

        protected override void Dispose()
        {
            NavigationStore.CurrentViewChanged -= OnCurrentViewChanged;
        }
    }
}

