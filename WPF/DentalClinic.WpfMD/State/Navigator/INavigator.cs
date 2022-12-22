using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;

namespace DentalClinic.WpfMD.State.Navigator
{
    public interface INavigator
    {
        IViewType CurrentViewModel { get; set; }
        IAsyncCommand<ViewType> ChangeCurrentView { get; }
    }
}
