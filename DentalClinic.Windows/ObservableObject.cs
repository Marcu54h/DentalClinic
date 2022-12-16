namespace DentalClinic.Windows
{

    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Fields



        #endregion // Fields

        #region Properties



        #endregion // Properties

        #region Constructors



        #endregion // Constructors

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // Methods
    }
}
