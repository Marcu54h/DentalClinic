namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class NewVisitViewControlViewModel : BaseViewModel
    {
        #region Fields

        private bool dataCorrect = false;

        private ICommand commandToFilter;

        private ICommand commandToGoBack;

        private ICommand commandToScheduleVisit;

        private string filter = "";

        private int filterBy = 0; // LastName - "NAZWISKA"

        private readonly string grayColor = "#808080";

        private readonly string redColor = "#DC143C";

        private string itIsNotMyEmployee = "#808080";

        private string visitDateLabel;

        #endregion // Fields

        #region Properties

        public bool DataCorrect
        {
            get { return dataCorrect; }
            private set { dataCorrect = value; NotifyPropertyChanged(nameof(DataCorrect)); }
        }

        public ICollection<Employee> DoctorCollection
        {
            get
            {
                return MainDataContext.MainContext.Employees
                                                  .Include("Person")
                                                  .OrderBy(x => x.Person.LastName)
                                                  .ToArray();
            }
        }

        public ICollection<Patient> FilteredPatientCollection { get; set; } = new ObservableCollection<Patient>();

        public ICollection<Office> OfficeCollection
        {
            get
            {
                return MainDataContext.MainContext.Offices.OrderBy(x => x.Number).ToArray();
            }
        }

        public ICollection<Patient> PatientCollection
        {
            get
            {
                return MainDataContext.MainContext.Patients.Include("Person").OrderBy(x => x.Person.LastName).ToArray();
            }
        }

        public int FilterBy
        {
            get { return filterBy; }
            set { filterBy = value; NotifyProperitesChanged(nameof(FilterBy)); refresh(); }
        }

        public Office SelectedOffice
        {
            get { return App.container.Resolve<Container>().SelectedOffice; }
            set
            {
                App.container.Resolve<Container>().SelectedOffice = value;
                NotifyPropertyChanged(nameof(SelectedOffice));
                updateVisitData();
            }
        }

        public Visit SelectedVisit
        {
            get { return App.container.Resolve<Container>().SelectedVisit; }
            set
            {
                App.container.Resolve<Container>().SelectedVisit = value;
                NotifyPropertyChanged(nameof(SelectedVisit));
            }
        }

        public Employee SelectedEmployee
        {
            get { return App.container.Resolve<Container>().SelectedEmployee; }
            set
            {
                App.container.Resolve<Container>().SelectedEmployee = value;
                NotifyPropertyChanged(nameof(SelectedEmployee));
                updateVisitData();
            }
        }

        public Patient Patient
        {
            get { return App.container.Resolve<Container>().SelectedPatient; }
            set
            {
                App.container.Resolve<Container>().SelectedPatient = value;
                NotifyPropertyChanged(nameof(Patient));

                updateVisitData();
            }
        }

        public WorkInterval SelectedWorkInterval
        {
            get
            {
                return App.container.Resolve<Container>().SelectedWorkInterval;
            }
            set
            {

                App.container.Resolve<Container>().SelectedWorkInterval = value;
                NotifyPropertyChanged(nameof(SelectedWorkInterval));
                refresh();
            }
        }
        public string Filter
        {
            get { return filter; }
            set { filter = value.ToLower(); NotifyProperitesChanged(nameof(Filter)); refresh(); }
        }

        

        public string ItIsNotMyEmployee
        {
            get { return itIsNotMyEmployee; }
            set
            {
                itIsNotMyEmployee = value;
                NotifyPropertyChanged(nameof(ItIsNotMyEmployee));
            }
        }

        public string VisitsTime
        {
            get { return "dn. " + SelectedWorkInterval.DateAndTime.ToShortDateString() + ", o godz. " +
                         SelectedWorkInterval.DateAndTime.ToShortTimeString(); }
        }

        public string VisitDateLabel
        {
            get { return visitDateLabel; }
            set { visitDateLabel = value; NotifyPropertyChanged(nameof(VisitDateLabel)); }
        }

        #endregion // Properties

        #region Constructors

        public NewVisitViewControlViewModel()
        {
            refresh();
        }

        #endregion // Constructors

        #region ICommands

        public ICommand CommandToFilter
        {
            get
            {
                if (commandToFilter is null)
                    commandToFilter = new ActionCommand(x =>
                    {
                        if (Filter != (string)x)
                        {
                            Filter = (string)x;
                            refresh();
                        }

                    });
                return commandToFilter;
            }
        }

        public ICommand CommandToGoBack
        {
            get
            {
                if (commandToGoBack is null)
                    commandToGoBack = new ActionCommand(x =>
                    {
                        App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                    });
                return commandToGoBack;
            }
        }

        public ICommand CommandToScheduleVisit
        {
            get
            {
                if (commandToScheduleVisit is null)
                    commandToScheduleVisit = new ActionCommand(x =>
                    {
                        if (DataCorrect)
                        {
                            Patient.Visits.Add(new Visit
                            {
                                Date = SelectedWorkInterval.DateAndTime,
                                Office = SelectedOffice,
                                Employee = SelectedEmployee
                            });

                            MainDataContext.MainContext.SaveChanges();

                            MessageBox.Show("Wizyta została umówiona.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);


                            App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                        }
                        
                    });
                return commandToScheduleVisit;
            }
        }

        #endregion

        #region Methods

        private void refresh()
        {
            if (!(SelectedWorkInterval is null))
            {
                VisitDateLabel = "Nowa wizyta - " + SelectedWorkInterval.DateAndTime.ToLongDateString() +
                                 " o godz. " + SelectedWorkInterval.LabelHour + ":" +
                                 SelectedWorkInterval.LabelMinute;
            }


            FilteredPatientCollection.Clear();

            FilteredPatientCollection.Clear();

            if (string.IsNullOrWhiteSpace(Filter))
            {
                PatientCollection.ToList().ForEach(x => FilteredPatientCollection.Add(x));
            }
            else
            {
                switch (FilterBy)
                {
                    case 0:
                        try { PatientCollection.ToList().FindAll(x => x.Person.LastName.ToLower().StartsWith(Filter)).ForEach(y => FilteredPatientCollection.Add(y)); } catch { }
                        break;
                    case 1:
                        try { PatientCollection.ToList().FindAll(x => x.Person.FirstName.ToLower().StartsWith(Filter)).ForEach(y => FilteredPatientCollection.Add(y)); } catch { }
                        break;
                    case 2:
                        try { PatientCollection.ToList().FindAll(x => x.Person.PersonalNumber.ToLower().StartsWith(Filter)).ForEach(y => FilteredPatientCollection.Add(y)); } catch { }
                        break;
                    case 3:
                        try { PatientCollection.ToList().FindAll(x => x.Person.Title.ToLower().StartsWith(Filter)).ForEach(y => FilteredPatientCollection.Add(y)); } catch { }
                        break;
                }
            }
        }

        public void Refresh()
        {
            refresh();
        }

        private void updateVisitData()
        {

            if (!(Patient is null) && SelectedEmployee is null)
            {
                SelectedEmployee = Patient.Employee;
            }

            if (!(Patient is null) &&
                SelectedEmployee == Patient.Employee)
            {
                ItIsNotMyEmployee = grayColor;
            }
            else
            {
                ItIsNotMyEmployee = redColor;
            }

            if (!(SelectedEmployee is null) && !(SelectedOffice is null) && !(Patient is null))
            {
                DataCorrect = true;

            }
            else
            {
                DataCorrect = false;
            }
                
        }

        #endregion // Methods
    }
}
