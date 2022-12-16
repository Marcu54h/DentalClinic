namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class AddVisitControlViewModel : BaseViewModel
    {
        #region Fields

        private ICommand commandToGoBack;

        private ICommand commandToScheduleVisit;

        private int selectedHour;

        private static int startWorkHour = App.container.Resolve<Container>().AppSettings.StartWorkHour;

        private static int endWorkHour = App.container.Resolve<Container>().AppSettings.EndWorkHour;

        private static int interval = App.container.Resolve<Container>().AppSettings.ScheduleInterval;

        #endregion // Fields

        #region Properties

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Office OfficeData { get; set; }
        public Employee EmployeeData { get; set; }
        public Patient PatientData { get; set; }

        public ICollection<Office> OfficeCollection
        {
            get
            {
                return MainDataContext.MainContext.Offices.ToArray();
            }
        }

        public ICollection<int> HourCombo { get; } = Enumerable.Range
            (
                startWorkHour,
                endWorkHour - startWorkHour + 1
            ).ToArray();

        public ICollection<int> MinuteCombo { get; } = new Collection<int>();

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

        public ICollection<Patient> PatientCollection
        {
            get
            {
                return MainDataContext.MainContext.Patients.ToArray();
            }
        }

        public DateTime SelectedDate { get; set; } = DateTime.Today;

        public int SelectedHour
        {
            get { return selectedHour; }

            set
            {
                if (value < startWorkHour)
                    SelectedHour = HourCombo.First();
                else if (value > endWorkHour)
                    SelectedHour = HourCombo.Last();
                else
                    selectedHour = value;
                NotifyPropertyChanged(nameof(SelectedHour));
            }           
        }

        public int SelectedQuarter { get; set; }

        public Employee SelectedEmployee { get; set; }

        public Office SelectedOffice { get; set; }

        public Patient Patient { get; set; }



        #endregion // Properties

        #region Constructors

        public AddVisitControlViewModel()
        {
            for (int i = 0; i < 60; i += interval) MinuteCombo.Add(i);

            SelectedHour = DateTime.Now.Hour;
        }


        #endregion // Constructors

        #region ICommands

        public ICommand CommandToGoBack
        {
            get
            {
                if (commandToGoBack is null)
                    commandToGoBack = new ActionCommand(x => App.container.Resolve<IKnowWhoCall>().WhoCalledMe());
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

                        Date = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedHour, SelectedQuarter, 0);

                        Visit newVisit = new Visit
                        {
                            Date = Date,
                            Office = SelectedOffice,
                            Employee = SelectedEmployee,
                            Patient = Patient
                        };

                        MainDataContext.MainContext.Visits.Add(newVisit);

                        MainDataContext.MainContext.SaveChanges();

                        App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                    });
                return commandToScheduleVisit;
            }
        }


        #endregion // ICommands

        #region Methods



        #endregion // Methods
    }
}
