namespace DentalClinic.Data
{

    using System;

    /// <summary>
    /// 
    /// </summary>
    public class EmployeeWrapper : IProvideEmployeeData
    {
        #region Fields

        #endregion // Fields

        #region Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public string PWZNumber { get; set; }

        public string FavoriteColor { get; set; }

        public string Caption => Title + " " + FirstName + " " + LastName;

        #endregion // Properties

        #region Constructors

        public EmployeeWrapper()
        {
        }

        public EmployeeWrapper(Employee employee)
        {
            Id = employee.Id;
            Title = employee.Person.Title;
            FirstName = employee.Person.FirstName;
            LastName = employee.Person.LastName;
            PersonalNumber = employee.Person.PersonalNumber;
            PWZNumber = employee.PWZNumer;
            FavoriteColor = employee.FavoriteColor;
        }

        #endregion // Constructors

        #region Methods

        public IProvideEmployeeData Interface => this;
       

        #endregion // Methods
        
    }
}
