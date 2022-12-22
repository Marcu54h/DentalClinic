using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.State.Navigator;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DentalClinic.WpfMD.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IViewType _currentView = default!;
        private readonly INavigationStore _navigationStore;

        public ViewModelBase(INavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        protected virtual void Changed([CallerMemberName]string propertyName = null!) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        protected bool SetField<T>(ref T field, T value, [CallerMemberName]string propertyName = null!)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            Changed(propertyName);
            return true;
        }

        public IViewType CurrentView
        {
            get => _currentView;
            set => SetField(ref _currentView, value);
        }

        protected virtual void Dispose()
        {

        }

        protected INavigationStore NavigationStore => _navigationStore;
    }
}
