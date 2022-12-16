namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Collections;
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
    public class AddPatientControlViewModel : BaseViewModel
    {
        #region Fields

        private ICommand commandToAddAddress;

        private ICommand commandToAddPatient;

        private ICommand commandToGoBack;

        private ICommand commandToRemoveAddress;

        #endregion // Fields

        #region Properties

        [MinLength(2)]
        public string Title
        {
            get { return NewPatient.Person.Title; }
            set { NewPatient.Person.Title = value; }
        }
        [Required, MinLength(2)]
        public string FirstName
        {
            get { return NewPatient.Person.FirstName; }
            set
            {
                NewPatient.Person.FirstName = value is null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
            }
        }
        [Required, MinLength(2)]
        public string LastName
        {
            get { return NewPatient.Person.LastName; }
            set { NewPatient.Person.LastName = value is null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value); }
        }
        [MinLength(11), MaxLength(11)]
        public string PersonalNumber
        {
            get { return NewPatient.Person.PersonalNumber; }
            set { NewPatient.Person.PersonalNumber = value; }
        }
    
        public bool? Insured { get; set; }

        public string InsuranceNumber { get; set; }

        public bool? Disabled { get; set; }

        public string DisabilityType { get; set; }

        public Employee SelectedEmployee
        {
            get { return NewPatient.Employee; }
            set
            {
                NewPatient.Employee = value;
                NotifyProperitesChanged(nameof(SelectedEmployee));
            }
        }

        public ICollection<Address> AddressCollection
        {
            get { return NewPatient.Person.Addresses; }
            set { NewPatient.Person.Addresses = value; }
        }



        public ICollection<Employee> DoctorList
        {
            get
            {
                return MainDataContext.MainContext.Employees.Include("Person").ToArray();
            }
        }

        public Patient NewPatient { get; set; }
       
        #endregion // Properties

        #region Constructors

        public AddPatientControlViewModel()
        {
            NewPatient = new Patient { AddDate = DateTime.Now, ModifiedDate = DateTime.Now };

            NewPatient.Person = new Person { AddDate = DateTime.Now, ModifiedDate = DateTime.Now };

            NewPatient.Person.Addresses = new Collection<Address>();

            NewPatient.Employee = new Employee { AddDate = DateTime.Now, ModifiedDate = DateTime.Now };

            AddressCollection = new ObservableCollection<Address>(NewPatient.Person.Addresses);

            SelectedEmployee = null;
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
                        Address newAddress = new Address { ModifiedDate = DateTime.Now, AddressType = "prywatny" };
                        NewPatient.Person.Addresses.Add(newAddress);
                    });
                return commandToAddAddress;
            }
        }

        public ICommand CommandToAddPatient
        {
            get
            {
                if (commandToAddPatient == null)
                    commandToAddPatient = new ActionCommand(x =>
                    {
                        MainDataContext.MainContext.Patients.Add(NewPatient);

                        MainDataContext.MainContext.SaveChanges();

                        App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                    });
                return commandToAddPatient;
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
                            Address selectedAddres = x as Address;

                            NewPatient.Person.Addresses.Remove(selectedAddres);

                            selectedAddres.Person = null;

                            try
                            {
                                MainDataContext.MainContext.Addresses.Remove(selectedAddres);
                            }
                            catch
                            {

                            }
                        }
                    });
                return commandToRemoveAddress;
            }
        }

        #endregion // ICommands
    }
}
