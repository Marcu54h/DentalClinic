using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DentalClinic.WpfMD.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void Changed([CallerMemberName]string propertyName = null!) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        protected bool SetField<T>(ref T field, T value, [CallerMemberName]string propertyName = null!)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            Changed(propertyName);
            return true;
        }
    }
}
