// -----------------------------------------------------------------------------
//  <copyright file="BaseViewModel.cs" company="Adam Marzec">
//      Copyright (c) Adam Marzec, All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace DentalClinic.Wpf
{

    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void NotifyProperitesChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
