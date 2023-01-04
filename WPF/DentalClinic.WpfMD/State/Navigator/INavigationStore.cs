using DentalClinic.WpfMD.Abstraction;

namespace DentalClinic.WpfMD.State.Navigator
{
    public interface INavigationStore
    {
        IViewType CurrentView { get; set; }
        IViewType Back();
        IViewType Forward();
    }
}