using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using System.ComponentModel;

namespace DentalClinic.WpfMD.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Changed(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
