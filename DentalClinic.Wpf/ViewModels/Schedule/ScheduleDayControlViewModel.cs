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
    public class ScheduleDayControlViewModel : BaseViewModel, IControlDaySchedule
    {
        #region Fields

        private bool visitHasBeenChoosen = false;

        private ICommand commandToCheckThisEmployee;

        private ICommand commandToCheckThisOffice;

        private ICommand commandToDoubleClick;

        private ICommand commandToGoBack;

        private ICommand commandToMouseDown;

        private ICommand commandToNextDay;

        private ICommand commandToPreviousDay;

        private ICommand commandToSelectionChange;

        private ICommand commandToToday;

        private ICommand commandToUnCheckThisEmployee;

        private ICommand commandToUnCheckThisOffice;

        private int defaultSortByIndex = Properties.Settings.Default.DefaultSortBy;

        private int selectedEmployeeIndex;

        private int selectedOfficeIndex;

        private int selectedVisitIndex;

        private int selectedWorkIntervalIndex;

        private int startHour = App.container.Resolve<Container>().AppSettings.StartWorkHour;

        private int finishHour = App.container.Resolve<Container>().AppSettings.EndWorkHour;

        // interval must divide hour (60 minutes) to equal parts (60 / 15 = 4)
        private int interval = App.container.Resolve<Container>().AppSettings.ScheduleInterval;

        private WorkInterval selectedWorkInterval;

        #endregion // Fields

        #region Properties

        public DateTime DateTimePickerDate
        {
            get { return SelectedDay.Date; }
            set
            {
                SelectedDay.Date = value;

                Refresh();
            }
        }

        public int SelectedVisitIndex
        {
            get { return selectedVisitIndex; }
            set { selectedVisitIndex = value; NotifyPropertyChanged(nameof(SelectedVisitIndex)); }
        }

        public int SelectedWorkIntervalIndex
        {
            get { return selectedWorkIntervalIndex; }
            set { selectedWorkIntervalIndex = value; NotifyPropertyChanged(nameof(SelectedWorkIntervalIndex)); }
        }

        public int SelectedEmployeeIndex
        {
            get { return selectedEmployeeIndex; }
            set { selectedEmployeeIndex = value; NotifyPropertyChanged(nameof(SelectedEmployeeIndex)); }
        }

        public int SelectedOfficeIndex
        {
            get { return selectedOfficeIndex; }
            set { selectedOfficeIndex = value; NotifyPropertyChanged(nameof(SelectedEmployeeIndex)); }
        }

        public ICollection<EmployeeWithSelection> EmployeeCollection { get; } = new Collection<EmployeeWithSelection>();

        public ICollection<OfficeWithSelection> OfficeCollection { get; } = new Collection<OfficeWithSelection>();

        public ICollection<EmployeeWithSelection> SelectedEmployees { get; set; } = new ObservableCollection<EmployeeWithSelection>();

        public ICollection<OfficeWithSelection> SelectedOffices { get; set; } = new ObservableCollection<OfficeWithSelection>();

        public int NumberOfQuarters { get { return (finishHour - startHour) * (60 / interval); } }

        public int SortByIndex
        {
            get { return defaultSortByIndex; }
            set
            {
                defaultSortByIndex = value;
                NotifyPropertyChanged(nameof(SortByIndex));
                Properties.Settings.Default.DefaultSortBy = defaultSortByIndex;
                Properties.Settings.Default.Save();
            }
        }

        public IScheduleDay SelectedDay
        {
            get { return App.container.Resolve<Container>().SelectedScheduleDay; }
            set
            {
                App.container.Resolve<Container>().SelectedScheduleDay = value;

                NotifyPropertyChanged(nameof(SelectedDay));

                NotifyPropertyChanged(nameof(DateTimePickerDate));

                NotifyPropertyChanged(nameof(SelectedDayLabel));
            }
        }

        public Visit SelectedVisit
        {
            get { return App.container.Resolve<Container>().SelectedVisit; }
            set
            {
                App.container.Resolve<Container>().SelectedVisit = value;
                NotifyPropertyChanged(nameof(SelectedVisit));
                selectedWorkInterval = null;
                NotifyPropertyChanged(nameof(SelectedWorkInterval));
            }
        }

        public WorkInterval SelectedWorkInterval
        {
            get { return App.container.Resolve<Container>().SelectedWorkInterval; }
            set
            {
                App.container.Resolve<Container>().SelectedWorkInterval = value;
                NotifyPropertyChanged(nameof(selectedWorkInterval));
                App.container.Resolve<Container>().SelectedVisit = null;
                NotifyPropertyChanged(nameof(SelectedVisit));
            }
        }

        public string SelectedDayLabel
        {
            get { return SelectedDay.Date.ToString("dddd, dd MMMM yyyy"); }
        }

        #endregion // Properties

        #region Constructors

        public ScheduleDayControlViewModel()
        {
            SelectedDay = App.container.Resolve<Container>().SelectedScheduleDay;

            refreshSelectedEmployees();

            refreshSelectedOffices();
        }

        #endregion // Constructors

        #region ICommands

        public ICommand CommandToCheckThisEmployee
        {
            get
            {
                if (commandToCheckThisEmployee is null)
                    commandToCheckThisEmployee = new ActionCommand(x =>
                    {
                        ((EmployeeWithSelection)x).Selected = true;
                        SelectedEmployees.Add((EmployeeWithSelection)x);
                    });
                return commandToCheckThisEmployee;
            }
        }

        public ICommand CommandToCheckThisOffice
        {
            get
            {
                if (commandToCheckThisOffice is null)
                    commandToCheckThisOffice = new ActionCommand(x =>
                    {
                        ((OfficeWithSelection)x).Selected = true;
                        SelectedOffices.Add((OfficeWithSelection)x);
                    });
                return commandToCheckThisOffice;
            }
        }

        public ICommand CommandToDoubleClick
        {
            get
            {
                if (commandToDoubleClick is null)
                    commandToDoubleClick = new ActionCommand(x =>
                    {
                        if (x is Visit)
                        {
                            visitHasBeenChoosen = true;

                            try { App.container.Resolve<Container>().SelectedTeeth.Clear(); } catch { }

                            Visit v = MainDataContext.MainContext.Visits
                                                                 .Include("Comments")
                                                                 .Include("Treatments")
                                                                 .Include("Treatments.SubTreatment")
                                                                 .Include("Treatments.SubTreatment.Sub2Treatment")
                                                                 .Include("Teeth")
                                                                 .Include("Teeth.Comments")
                                                                 .Include("Teeth.Treatments")
                                                                 .Include("Teeth.Treatments.SubTreatment")
                                                                 .Include("Teeth.Treatments.SubTreatment.Sub2Treatment")
                                                                 .Include("Patient")
                                                                 .Include("Patient.Person")
                                                                 .Include("Employee")
                                                                 .Include("Employee.Person")
                                                                 .Include("Office")
                                                                 .Where(y => y.Id == ((Visit)x).Id)
                                                                 .FirstOrDefault();

                            App.container.Resolve<Container>().SelectedVisit = (v);

                            App.container.Resolve<Container>().SelectedPatient = (v.Patient);

                            App.container.Resolve<IKnowWhoCall>().Call<VisitViewControl, ScheduleDayControl>();
                        }

                        if (x is WorkInterval)
                        {
                            if (!visitHasBeenChoosen)
                            {
                                App.container.Resolve<Container>().SelectedVisit = null;

                                App.container.Resolve<Container>().SelectedWorkInterval = (WorkInterval)x;

                                App.container.Resolve<IKnowWhoCall>().Call<NewVisitViewControl, ScheduleDayControl>();

                            }
                            visitHasBeenChoosen = false;
                        }
                        if (x is OfficeWithSelection)
                        {
                            App.container.Resolve<Container>().SelectedOffice = ((OfficeWithSelection)x).Office;
                        }
                        if (x is EmployeeWithSelection)
                        {
                            App.container.Resolve<Container>().SelectedEmployee = ((EmployeeWithSelection)x).Employee;
                        }
                        
                    });
                return commandToDoubleClick;
            }
        }

        public ICommand CommandToGoBack
        {
            get
            {
                if (commandToGoBack is null)
                    commandToGoBack = new ActionCommand(x => App.container.Resolve<IKnowWhoCall>().WhoCalledMe());
                return commandToGoBack;
            }
        }

        public ICommand CommandToMouseDown
        {
            get
            {
                if (commandToMouseDown is null)
                    commandToMouseDown = new ActionCommand(x =>
                    {
                        if (x is VisitWrapper)
                        {
                            SelectedWorkIntervalIndex = -1;
                        }
                        
                        if (x is WorkInterval)
                        {
                            SelectedVisitIndex = -1;
                        }
                        
                    });
                return commandToMouseDown;
            }
        }

        public ICommand CommandToNextDay
        {
            get
            {
                if (commandToNextDay is null)
                    commandToNextDay = new ActionCommand(x =>
                    {
                        SelectedDay.Date = SelectedDay.Date.AddDays(1);
                        Refresh();
                    });
                return commandToNextDay;
            }
        }

        public ICommand CommandToPreviousDay
        {
            get
            {
                if (commandToPreviousDay is null)
                    commandToPreviousDay = new ActionCommand(x =>
                    {
                        SelectedDay.Date = SelectedDay.Date.AddDays(-1);
                        Refresh();
                    });
                return commandToPreviousDay;
            }
        }

        public ICommand CommandToSelectionChange
        {
            get
            {
                if (commandToSelectionChange is null)
                    commandToSelectionChange = new ActionCommand(x =>
                    {
                        if ((string)x == "Gabinetów")
                        {
                            
                        }
                        if((string)x == "Pracowników")
                        {

                        }
                    });
                return commandToSelectionChange;
            }
        }

        public ICommand CommandToToday
        {
            get
            {
                if (commandToToday is null)
                    commandToToday = new ActionCommand(x =>
                    {
                        SelectedDay.Date = DateTime.Today;
                        Refresh();
                    });
                return commandToToday;
            }
        }

        public ICommand CommandToUnCheckThisEmployee
        {
            get
            {
                if (commandToUnCheckThisEmployee is null)
                    commandToUnCheckThisEmployee = new ActionCommand(x =>
                    {
                        ((EmployeeWithSelection)x).Selected = false;
                        SelectedEmployees.Remove((EmployeeWithSelection)x);
                    });
                return commandToUnCheckThisEmployee;
            }
        }

        public ICommand CommandToUnCheckThisOffice
        {
            get
            {
                if (commandToUnCheckThisOffice is null)
                    commandToUnCheckThisOffice = new ActionCommand(x =>
                    {
                        ((OfficeWithSelection)x).Selected = false;
                        SelectedOffices.Remove((OfficeWithSelection)x);
                    });
                return commandToUnCheckThisOffice;
            }
        }

        #endregion // ICommands

        #region Methods

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(SelectedDayLabel));
            NotifyPropertyChanged(nameof(DateTimePickerDate));
            refreshSelectedEmployees();
            refreshSelectedOffices();
        }

        private void refreshSelectedEmployees()
        {
            Dictionary<int, bool> SelectionStatus = new Dictionary<int, bool>();

            if (EmployeeCollection.Count > 0)
                foreach (EmployeeWithSelection employeeWithSelection in EmployeeCollection)
                {
                    SelectionStatus.Add(employeeWithSelection.Id, employeeWithSelection.Selected);
                }


            EmployeeCollection.Clear();
            SelectedEmployees.Clear();

            MainDataContext.MainContext.Employees.OrderBy(x => x.Person.LastName).ToList().ForEach(y =>
            {
                EmployeeWithSelection employeeWithSelection = new EmployeeWithSelection(y, SelectedDay.Date, startHour, finishHour, interval);

                EmployeeCollection.Add(employeeWithSelection);

                try { employeeWithSelection.Selected = SelectionStatus[employeeWithSelection.Id]; } catch { }

                if (employeeWithSelection.Selected)
                    SelectedEmployees.Add(employeeWithSelection);
            });
            CollectionViewSource.GetDefaultView(EmployeeCollection).Refresh();
        }

        private void refreshSelectedOffices()
        {
            Dictionary<int, bool> SelectionStatus = new Dictionary<int, bool>();

            if (OfficeCollection.Count > 0)
                foreach (OfficeWithSelection officeWithSelection in OfficeCollection)
                {
                    SelectionStatus.Add(officeWithSelection.Id, officeWithSelection.Selected);
                }

            OfficeCollection.Clear();
            SelectedOffices.Clear();

            MainDataContext.MainContext.Offices.OrderBy(x => x.Number).ToList().ForEach(y =>
            {
                OfficeWithSelection officeWithSelection = new OfficeWithSelection(y, SelectedDay.Date, startHour, finishHour, interval);

                OfficeCollection.Add(officeWithSelection);

                try { officeWithSelection.Selected = SelectionStatus[officeWithSelection.Id]; } catch { }

                if (officeWithSelection.Selected)
                    SelectedOffices.Add(officeWithSelection);
            });
            CollectionViewSource.GetDefaultView(OfficeCollection).Refresh();

        }

        #endregion // Methods
    }

    /// <summary>
    /// 
    /// </summary>
    public class OfficeWithSelection : IOfficeData
    {
        #region Fields

        private DateTime workDay;

        private int interval;

        private int finishHour;

        private int startHour;

        public Office Office;

        #endregion

        #region Properties

        public bool Selected { get; set; } = true;

        public ICollection<WorkInterval> WorkingHours { get; } = new Collection<WorkInterval>();

        #endregion

        #region IOfficeData
        public int Id { get => Office.Id; set => Office.Id = value; }

        public string Type { get => Office.Type; set => Office.Type = value; }

        public string Number { get => Office.Number; set => Office.Number = value; }

        public string Label { get => Office.Label; set => Office.Label = value; }

        public string Caption => Office.Number + " " + Office.Label;
        #endregion

        #region Constructor
        public OfficeWithSelection(Office office, DateTime dateTime, int startHour, int finishHour, int interval)
        {
            Office = office;
            this.interval = interval;
            this.startHour = startHour;
            this.finishHour = finishHour;
            workDay = dateTime;

            setWorkingHours();

            refreshVisits();
        }
        #endregion

        #region Methods

        private void setWorkingHours()
        {
            for (int i = startHour; i <= finishHour; i++)
            {
                for (int j = 0; j < 60; j += interval)
                {
                    WorkingHours.Add(new WorkInterval(new DateTime(workDay.Date.Year, workDay.Date.Month, workDay.Date.Day, i, j, 0)));
                }
            }
        }

        private void refreshVisits()
        {

            foreach (Visit visit in MainDataContext.MainContext.Visits
                                                            .Where(x =>
                                                                    x.Date.Year == workDay.Date.Year &&
                                                                    x.Date.Month == workDay.Date.Month &&
                                                                    x.Date.Day == workDay.Date.Day &&
                                                                    x.Office.Id == Id)
                                                            .ToArray())
            {
                WorkInterval tempHour = WorkingHours.Where(x => x.Hour == visit.Date.Hour && x.Minute == visit.Date.Minute).FirstOrDefault();
                if (!(tempHour is null))
                    tempHour.TryToAddVisit(visit);
            }
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class EmployeeWithSelection : IProvideEmployeeData
    {
        #region Fields

        private DateTime workDay;

        private int interval;

        private int finishHour;

        private int startHour;

        public Employee Employee;

        #endregion

        #region Properties

        public bool Selected { get; set; } = true;

        public ICollection<WorkInterval> WorkingHours { get; } = new Collection<WorkInterval>();

        #endregion

        #region IProvideEmployeeData

        public string Caption => Employee.Person.Title + " " + Employee.Person.LastName + " " + Employee.Person.FirstName;

        public int Id => Employee.Id;

        public string Title => Employee.Person.Title;

        public string FirstName => Employee.Person.FirstName;

        public string LastName => Employee.Person.LastName;

        public string PersonalNumber => Employee.Person.PersonalNumber;

        public string PWZNumber => Employee.PWZNumer;

        public string FavoriteColor => Employee.FavoriteColor;

        #endregion

        #region Constructor

        public EmployeeWithSelection(Employee employee, DateTime dateTime, int startHour, int finishHour, int interval)
        {
            Employee = employee;
            this.interval = interval;
            this.startHour = startHour;
            this.finishHour = finishHour;
            workDay = dateTime;

            setWorkingHours();

            refreshVisits();
        }

        #endregion

        #region Methods

        private void setWorkingHours()
        {
            for (int i = startHour; i <= finishHour; i++)
            {
                for (int j = 0; j < 60; j += interval)
                {
                    WorkingHours.Add(new WorkInterval(new DateTime(workDay.Date.Year, workDay.Date.Month, workDay.Date.Day, i, j, 0)));
                }
            }
        }

        private void refreshVisits()
        {
            foreach (Visit visit in MainDataContext.MainContext.Visits
                                                            .Where(x =>
                                                                    x.Date.Year == workDay.Date.Year &&
                                                                    x.Date.Month == workDay.Date.Month &&
                                                                    x.Date.Day == workDay.Date.Day &&
                                                                    x.Employee.Id == Id)
                                                            .ToArray())
            {
                WorkInterval tempHour = WorkingHours.Where(x => x.Hour == visit.Date.Hour && x.Minute == visit.Date.Minute).FirstOrDefault();
                if (!(tempHour is null))
                    tempHour.TryToAddVisit(visit);
            }
        }

        #endregion
    }
    
    /// <summary>
    /// Represents data for single time interval in day schedule.
    /// </summary>
    public class WorkInterval
    {
        private ICommand commandToScheduleNewPatient;

        internal DateTime DateAndTime;
        public int Hour { get { return DateAndTime.Hour; } }
        public int Minute { get { return DateAndTime.Minute; } }
        public string LabelHour { get { return DateAndTime.ToString("HH"); } }
        public string LabelMinute { get { return DateAndTime.ToString("mm"); } }
        public ICollection<Visit> Visits { get; set; } = new Collection<Visit>();

        public WorkInterval(DateTime dateTime)
        {
            DateAndTime = dateTime;
        }

        public void TryToAddVisit(Visit visit)
        {
            if (visit.Date.Year == DateAndTime.Year &&
                visit.Date.Month == DateAndTime.Month &&
                visit.Date.Day == DateAndTime.Day &&
                visit.Date.Hour == DateAndTime.Hour &&
                visit.Date.Minute == DateAndTime.Minute)
                Visits.Add(visit);
        }

        public ICommand CommandToScheduleNewPatient
        {
            get
            {
                if (commandToScheduleNewPatient is null)
                    commandToScheduleNewPatient = new ActionCommand(x =>
                    {
                        App.container.Resolve<Container>().SelectedWorkInterval = this;
                        App.container.Resolve<IKnowWhoCall>().Call<ScheduleNewPatientControl, ScheduleDayControl>();
                    });
                return commandToScheduleNewPatient;
            }
        }
    }
}
