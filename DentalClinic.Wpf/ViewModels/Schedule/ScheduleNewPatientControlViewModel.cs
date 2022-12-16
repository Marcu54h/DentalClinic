namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class ScheduleNewPatientControlViewModel : BaseViewModel
    {
        #region Fields

        private ICommand commandToGoBack;

        private ICommand commandToScheduleNewPatient;

        private int interval = Properties.Settings.Default.ScheduleInterval;

        [Required, MinLength(2)]
        private string firstName;
        [Required, MinLength(2)]
        private string lastName;

        private string cellPhone;

        #endregion // Fields

        #region Properties

        private bool CorrectData
        {
            get
            {
                if (!(firstName is null) && !(lastName is null))
                {
                    if (firstName.Length > 2 && lastName.Length > 2 &&
                        !(SelectedEmployee is null) && !(SelectedOffice is null))
                    {
                        return true;
                    }
   
                }
                return false;
            }
        }

        public DateTime VisitDateTime
        {
            get { return App.container.Resolve<Container>().SelectedWorkInterval.DateAndTime; }
            set { App.container.Resolve<Container>().SelectedWorkInterval.DateAndTime = value; }
            
        }

        public ICollection<int> HourCombo { get; } = Enumerable.Range
            (
                Properties.Settings.Default.StartWorkHour,
                Properties.Settings.Default.EndWorkHour - Properties.Settings.Default.StartWorkHour + 1
            ).ToArray();

        public ICollection<int> MinuteCombo { get; set; } = new Collection<int>();

        public ICollection<Office> OfficeCollection
        {
            get { return MainDataContext.MainContext.Offices.ToArray(); }
        }

        public ICollection<Employee> EmployeeCollection
        {
            get { return MainDataContext.MainContext.Employees.Include("Person").ToArray(); }
        }

        public int SelectedHour { get; set; }

        public int SelectedQuarter { get; set; }

        public Office SelectedOffice
        {
            get { return App.container.Resolve<Container>().SelectedOffice; }
            set { App.container.Resolve<Container>().SelectedOffice = value; }
        }

        public Employee SelectedEmployee
        {
            get { return App.container.Resolve<Container>().SelectedEmployee; }
            set { App.container.Resolve<Container>().SelectedEmployee = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value is null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
                NotifyPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value is null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
                NotifyPropertyChanged(nameof(LastName));
            }
        }

        public string CellPhone
        {
            get { return cellPhone; }
            set
            {
                cellPhone = value;
                NotifyPropertyChanged(nameof(CellPhone));
            }
        }

         #endregion // Properties

        #region Constructors

        public ScheduleNewPatientControlViewModel()
        {
            for (int i = 0; i < 60; i += interval) MinuteCombo.Add(i);

            SelectedHour = VisitDateTime.Hour;
            NotifyPropertyChanged(nameof(SelectedHour));

            SelectedQuarter = VisitDateTime.Minute;
            NotifyPropertyChanged(nameof(SelectedQuarter));
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

        public ICommand CommandToScheduleNewPatient
        {
            get
            {
                if (commandToScheduleNewPatient is null)
                    commandToScheduleNewPatient = new ActionCommand(x =>
                    {
                        if (CorrectData)
                        {

                            Person newPerson = new Person
                            {
                                FirstName = FirstName,
                                LastName = LastName,
                                AddDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };
                            newPerson.Patient = new Patient { AddDate = DateTime.Now,
                                                                ModifiedDate = DateTime.Now,
                                                                Employee = SelectedEmployee
                            };

                            if (!string.IsNullOrWhiteSpace(CellPhone))
                            {
                                newPerson.Addresses.Add(new Address { CellPhone = CellPhone, ModifiedDate = DateTime.Now });
                            }

                            newPerson.Patient.Visits.Add(new Visit
                            {
                                    
                                Date = new DateTime(VisitDateTime.Year, VisitDateTime.Month, VisitDateTime.Day, SelectedHour, SelectedQuarter, 0),
                                Employee = SelectedEmployee,
                                Office = SelectedOffice
                            });

                            try
                            {
                                MainDataContext.MainContext.People.Add(newPerson);

                                MainDataContext.MainContext.SaveChanges();

                                MessageBox.Show("Nowy pacjent został umuwiony na wizytę.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            catch
                            {
                                MessageBox.Show("Coś poszło nie tak.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            }

                            

                            App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                        }
                    });
                return commandToScheduleNewPatient;
            }
        }

        #endregion // ICommands

        #region Methods



        #endregion // Methods

    }
}
