using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State.Navigator;

namespace DentalClinic.WpfMD.ViewModels
{
    public partial class MainViewModel : ObservableRecipient, IViewType
    {
        [ObservableProperty]
        private IViewType currentView = default!;
        public ViewType ViewType => ViewType.MainView;
        
        private readonly IViewModelsFactory _viewModelsFactory;
        private readonly INavigationStore _navigationStore;

        [ObservableProperty]
        private bool isLeftDrawerOpen = false;

        public MainViewModel(IViewModelsFactory viewModelsFactory, INavigationStore navigationStore)
        {
            _viewModelsFactory = viewModelsFactory;
            _navigationStore = navigationStore;
        }

        [RelayCommand]
        private void ChangeView(ViewType viewType)
        {
            _navigationStore.CurrentView = _viewModelsFactory.Create(viewType);
            CurrentView = _navigationStore.CurrentView;
            IsLeftDrawerOpen = false;
        }

        [RelayCommand]
        private void OpenLeftDrawer(bool open)
        {
            IsLeftDrawerOpen = open;
        }
    }
}

