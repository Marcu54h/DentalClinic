using DentalClinic.WpfMD.Abstraction;
using System.Threading.Tasks;

namespace DentalClinic.WpfMD.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool isLeftDrawerOpen = false;
        public object CurrentView { get; set; }

        public bool IsLeftDrawerOpen
        {
            get => isLeftDrawerOpen;
            set
            {
                isLeftDrawerOpen = value;
                Changed(nameof(IsLeftDrawerOpen));
            }
        }

        public MainViewModel()
        {
            CurrentView = new PatientsViewModel();

            CommandToOpenLeftDrawer = new AsyncCommand<bool>(
                async (choice) => await Task.Run(() => IsLeftDrawerOpen = choice));
        }

        public IAsyncCommand<bool> CommandToOpenLeftDrawer { get; set; } 
    }
}
