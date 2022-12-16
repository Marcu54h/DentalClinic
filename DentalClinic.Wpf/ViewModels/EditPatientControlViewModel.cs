namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class EditPatientControlViewModel : BaseViewModel
    {
        #region Fields

        private ICommand commandToAddAddress;

        private ICommand commandToGoBack;

        private ICommand commandToRemoveAddress;

        private ICommand commandToSaveChanges;

        private ICommand commandToSetEmployeeId;

        #endregion // Fields

        #region Properties
        [MinLength(2)]
        public string Title
        {
            get { return Patient.Person.Title; }
            set { Patient.Person.Title = value; }
        }
        [Required, MinLength(2)]
        public string FirstName
        {
            get { return Patient.Person.FirstName; }
            set { Patient.Person.FirstName = value is null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value); }
        }
        [Required, MinLength(2)]
        public string LastName
        {
            get { return Patient.Person.LastName; }
            set { Patient.Person.LastName = value is null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value); }
        }
        [MinLength(11), MaxLength(11)]
        public string PersonalNumber
        {
            get { return Patient.Person.PersonalNumber; }
            set { Patient.Person.PersonalNumber = value; }
        }
    
        public bool? Insured { get; set; }

        public string InsuranceNumber
        {
            get { return Patient.InsuranceNumber; }
            set { Patient.InsuranceNumber = value; }
        }


        public bool? Disabled { get; set; }

        public string DisabilityType { get; set; }

        public int Id { get; set; }

        public Employee SelectedEmployee
        {
            get { return Patient.Employee; }
            set
            {
                Patient.Employee = value;
            }
        }

        public ICollection<Address> AddressCollection
        {
            get { return Patient.Person.Addresses; }
            set { Patient.Person.Addresses = value; }
        }

        public ICollection<Employee> DoctorList
        {
            get
            {
                return MainDataContext.MainContext.Employees.Include("Person").ToArray();
            }
        }


        public Patient Patient
        {
            get { return App.container.Resolve<Container>().SelectedPatient; }
            set
            {
                App.container.Resolve<Container>().SelectedPatient = value;
                NotifyPropertyChanged(nameof(Patient));

            }
        }

        #endregion // Properties

        #region Constructors

        public EditPatientControlViewModel()
        {
            AddressCollection = new ObservableCollection<Address>(Patient.Person.Addresses);
        }

        #endregion // Constructors

        #region Methods

 
        #endregion // Methods

        #region ICommands

        public ICommand CommandToAddAddress
        {
            get
            {
                if (commandToAddAddress == null)
                    commandToAddAddress = new ActionCommand(x =>
                    {
                        Address newAddress = new Address
                        {
                            ModifiedDate = DateTime.Now,
                            AddressType = "prywatny"
                        };

                        Patient.Person.Addresses.Add(newAddress);
                    });
                return commandToAddAddress;
            }
        }

        public ICommand CommandToSaveChanges
        {
            get
            {
                if (commandToSaveChanges == null)
                    commandToSaveChanges = new ActionCommand(x =>
                    {
                        MainDataContext.MainContext.SaveChanges();

                        App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                    });
                    return commandToSaveChanges;
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

        public ICommand CommandToRemoveAddress
        {
            get
            {
                if (commandToRemoveAddress == null)
                    commandToRemoveAddress = new ActionCommand(x =>
                    {
                        if (!(x is null))
                        {
                            Patient.Person.Addresses.Remove(x as Address);
                            (x as Address).Person = null;

                            try
                            {
                                MainDataContext.MainContext.Addresses.Remove(x as Address);
                            }
                            catch
                            {

                            }
                        }
                    });
                return commandToRemoveAddress;
            }
        }

        public ICommand CommandToSetEmployee
        {
            get
            {
                if (commandToSetEmployeeId == null)
                    commandToSetEmployeeId = new ActionCommand(x =>
                    {
                        if (!((x as Employee) is null))
                        {
                            Patient.Employee = (x as Employee);
                        }
                        
                    });
                return commandToSetEmployeeId;
            }
        }

        #endregion // ICommands
    }
}
