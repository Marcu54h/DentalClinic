using DentalClinic.WpfMD.Abstraction;
using System;

namespace DentalClinic.WpfMD.State.Navigator
{
    public interface INavigationStore
    {
        IViewType CurrentView { get; set; }
        IViewType Back();
        IViewType Forward();
        event Action<IViewType> CurrentViewChanged;
    }
}