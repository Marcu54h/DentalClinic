namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using Unity;

    public class EmployeeControlViewModel : BaseViewModel
    {
        #region Fields

        private ICommand addEmployee;

        private ICommand commandToDeleteEmployee;

        #endregion // Fields

        #region Properties

        public ICollection<Employee> EmployeeCollection
        {
            get { return App.container.Resolve<Container>().Employees; }
            set { App.container.Resolve<Container>().Employees = value; }
        }

        public int NumberOfColumns { get; set; } = 1;

        public int NumberOfRows { get; set; } = 0;

        public Employee SelectedEmployee
        {
            get { return App.container.Resolve<Container>().SelectedEmployee; }
            set { App.container.Resolve<Container>().SelectedEmployee = value; }
        }

        #endregion // Properties

        #region Constructor

        public EmployeeControlViewModel()
        {
            App.container.Resolve<Container>().Employees = MainDataContext.MainContext.Employees.Include("Person").ToArray();

            EmployeeCollection = new ObservableCollection<Employee>(App.container.Resolve<Container>().Employees);

        }

        #endregion // Constructor

        #region ICommand

        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee is null)
                    addEmployee = new ActionCommand(x => App.container.Resolve<IKnowWhoCall>().Call<AddEmployeeControl, EmployeeControl>());
                return addEmployee;
            }
        }

        public ICommand CommandToDeleteEmployee
        {
            get
            {
                if (commandToDeleteEmployee is null)
                    commandToDeleteEmployee = new ActionCommand(x =>
                    {
                        if (!(SelectedEmployee is null))
                        {
                            MessageBoxResult result = MessageBox.Show("Czy napewno usunąć pracownika wraz z powiązaną zawarością?",
                                                                      "Pytanie", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                try
                                {
                                    MainDataContext.MainContext.Employees.Remove(SelectedEmployee);

                                    MainDataContext.MainContext.SaveChanges();

                                    EmployeeCollection.Remove(SelectedEmployee);

                                    MessageBox.Show("Pracownik został usunięty.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                catch
                                {
                                    MessageBox.Show("Coś poszło nie tak.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                }
                            }
                        }
                    });
                return commandToDeleteEmployee;
            }
        }

        #endregion // ICommand

        #region Methods

        #endregion // Methods
    }
}
