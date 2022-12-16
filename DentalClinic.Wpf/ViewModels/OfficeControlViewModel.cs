namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class OfficeControlViewModel : BaseViewModel
    {
        #region Fields

        private bool changesMade = false;

        private ICommand commandToAddOffice;

        private ICommand commandToEndEditing;

        private ICommand commandToRemoveOffice;

        private ICommand commandToSaveOffice;

        #endregion // Fields

        #region Properties

        public bool ChangesMade
        {
            get { return changesMade; }
            set { changesMade = value; NotifyPropertyChanged(nameof(ChangesMade)); }
        }

        public ICollection<Office> OfficeCollection
        {
            get { return App.container.Resolve<Container>().Offices; }
            set { App.container.Resolve<Container>().Offices = value; }
        }


        public Office SelectedOffice
        {
            get { return App.container.Resolve<Container>().SelectedOffice; }
            set { App.container.Resolve<Container>().SelectedOffice = value; }
        }

        #endregion // Properties

        #region Constructors

        public OfficeControlViewModel()
        {
            App.container.Resolve<Container>().Offices = MainDataContext.MainContext.Offices.ToArray();

            OfficeCollection = new ObservableCollection<Office>(App.container.Resolve<Container>().Offices);
        }

        #endregion // Constructors

        #region ICommands

        public ICommand CommandToAddOffice
        {
            get
            {
                if (commandToAddOffice is null)
                    commandToAddOffice = new ActionCommand(x =>
                    {
                        ChangesMade = true;

                        Office newOffice = new Office { Label = "Nowy", Number = "123", Type = "Stomatologiczny" };

                        OfficeCollection.Add(newOffice);

                        MainDataContext.MainContext.Offices.Add(newOffice);

                        NotifyPropertyChanged(nameof(ChangesMade));
                    });

                return commandToAddOffice;
            }
        }

        public ICommand CommandToEndEditing
        {
            get
            {
                if (commandToEndEditing is null)
                    commandToEndEditing = new ActionCommand(x =>
                    {
                        ChangesMade = true;
                    });
                return commandToEndEditing;
            }
        }
        public ICommand CommandToRemoveOffice
        {
            get
            {
                if (commandToRemoveOffice is null)
                    commandToRemoveOffice = new ActionCommand(x =>
                    {
                        if (!(SelectedOffice is null))
                        {
                            ICollection<Visit> officeVisits = MainDataContext.MainContext.Visits.Where(y => y.OfficeId == SelectedOffice.Id).ToArray();
                            if (officeVisits.Count() > 0)
                            {
                                MessageBox.Show("Nie można usunąć gabinetu dopuki nie zostaną odwołane powiązane z nim wizyty.", "Informacja",
                                                MessageBoxButton.OK, MessageBoxImage.Information );
                            }
                            else
                            {
                                try
                                {
                                    MainDataContext.MainContext.Offices.Remove(SelectedOffice);
                                    OfficeCollection.Remove(SelectedOffice);
                                    MessageBox.Show("Gabinet został usunięty.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                catch
                                {
                                    MessageBox.Show("Coś poszło nie tak.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                }
                            }
                            ChangesMade = true;
                            NotifyPropertyChanged(nameof(ChangesMade));

                        }
                    });

                return commandToRemoveOffice;
            }
        }

        public ICommand CommandToSaveOffice
        {
            get
            {
                if (commandToSaveOffice is null)
                    commandToSaveOffice = new ActionCommand(x =>
                    {
                        try
                        {
                            MainDataContext.MainContext.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("Coś poszło nie tak.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }

                        ChangesMade = false;
                    });
                return commandToSaveOffice;
            }
        }

        #endregion // ICommands

        #region Methods

        #endregion // Methods
    }
}
