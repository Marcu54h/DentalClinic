using DentalClinic.WpfMD.Abstraction;
using System;

namespace DentalClinic.WpfMD.State.Navigator
{
    public interface INavigationStore
    {
        IViewType CurrentView { get; set; }

        event Action<IViewType> CurrentViewChanged;
    }
}