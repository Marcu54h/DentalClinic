using DentalClinic.WpfMD.Abstraction;
using System;

namespace DentalClinic.WpfMD.State.Navigator
{
    public class NavigationStore
    {
        public event Action<IViewType> CurrentViewChanged = default!;
        private IViewType _currentView = default!;

        public IViewType CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnCurrentViewChanged(value);
            }
        }

        private void OnCurrentViewChanged(IViewType view) =>
            CurrentViewChanged?.Invoke(view);

    }

}
