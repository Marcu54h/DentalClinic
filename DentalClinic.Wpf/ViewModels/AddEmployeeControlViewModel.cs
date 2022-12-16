namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Globalization;
    using System.Windows.Input;
    using Unity;
    using System.Windows;

    /// <summary>
    /// 
    /// </summary>
    public class AddEmployeeControlViewModel : BaseViewModel
    {
        #region Fields

        private string firstName;

        private string lastName;

        private ICommand addEmployee;

        #endregion // Fields

        #region Properties

        public string Title { get; set; }

        [Required, MinLength(2)]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value is null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value); }
        }


        [Required, MinLength(2)]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value is null ? null : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value); }
        }

        [Required, MinLength(11), MaxLength(11)]
        public string PersonalNumber { get; set; }

        public string PWZNumber { get; set; }

        public string FavoriteColor { get; set; } = "White";

        #endregion // Properties

        #region Constructors

        public AddEmployeeControlViewModel()
        {
        }

        #endregion // Constructors

        #region Methods

        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee is null)
                    addEmployee = new ActionCommand(x =>
                    {
                        Person newPerson = MainDataContext.MainContext.People
                                                                      .Where(y => y.FirstName == FirstName &&
                                                                                  y.LastName == LastName &&
                                                                                  y.PersonalNumber == PersonalNumber)
                                                                      .FirstOrDefault();

                        if (newPerson is null)
                        {
                            newPerson = new Person
                            {
                                AddDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                Title = Title,
                                LastName = LastName,
                                FirstName = FirstName,
                                PersonalNumber = PersonalNumber
                            };
                        }

                        newPerson.Employee = new Employee
                        {
                            AddDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            PWZNumer = PWZNumber,
                            FavoriteColor = FavoriteColor
                        };

                        try
                        {
                            MainDataContext.MainContext.People.Add(newPerson);

                            MainDataContext.MainContext.SaveChanges();

                            MessageBox.Show("Pracownik został dodany.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch
                        {
                            MessageBox.Show("Coś poszło nie tak.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }

                        App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                    });
                return addEmployee;
            }
        }

        #endregion // Methods
    }
}
