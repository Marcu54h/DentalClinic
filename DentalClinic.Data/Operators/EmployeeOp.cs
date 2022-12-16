namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class EmployeeOp : IPerformEmployeeOperation
    {
        
        #region Fields



        #endregion // Fields

        #region Properties



        #endregion // Properties

        #region Constructors



        #endregion // Constructors

        #region Methods

        public void AddEmployee(IProvideEmployeeData employeeData)
        {
            using (PDContainer pd = new PDContainer())
            {
                
                // First search person with given data
                Person person = pd.People
                                  .Where(e => e.FirstName == employeeData.FirstName && e.LastName == employeeData.LastName)
                                  .Include(e => e.Employee)
                                  .FirstOrDefault();


                // If person exist upate data and modifiation date of this person
                if(person == null)
                {
                    person = new Person();
                    pd.People.Add(person);
                    person.AddDate = (DateTime.UtcNow).AddHours(1);
                }

                person.Title = employeeData.Title;
                person.FirstName = employeeData.FirstName;
                person.LastName = employeeData.LastName;
                person.PersonalNumber = employeeData.PersonalNumber;
                person.ModifiedDate = (DateTime.UtcNow).AddHours(1);

                if (person.Employee == null)
                {
                    person.Employee = new Employee
                    {
                        PWZNumer = employeeData.PWZNumber,
                        FavoriteColor = employeeData.FavoriteColor,
                        AddDate = person.ModifiedDate,
                        ModifiedDate = person.ModifiedDate
                    };
                }
                else
                {
                    person.Employee.PWZNumer = employeeData.PWZNumber;
                    person.ModifiedDate = (DateTime.UtcNow).AddHours(1);
                }
                

                pd.SaveChanges();
            }
        }

        public void DeleteEmployee(IProvideEmployeeData employeeData)
        {
            using (PDContainer pd = new PDContainer())
            {
                try
                {
                    pd.Employees.Remove(pd.Employees.Where(x => x.Id == employeeData.Id).First());
                    pd.SaveChanges();
                }
                catch { }
            }
        }

        public IProvideEmployeeData GetEmployee(int? employeeId)
        {
            using (PDContainer pd = new PDContainer())
            {
                return new EmployeeWrapper(pd.Employees.Where(x => x.Id == employeeId).FirstOrDefault()).Interface;
            }
        }

        public ICollection<IProvideEmployeeData> GetEmployeeCollection()
        {
            ICollection<IProvideEmployeeData> employees = new Collection<IProvideEmployeeData>();
            using (PDContainer pd = new PDContainer())
            {
                foreach (Employee e in pd.Employees.Include("Person"))
                    employees.Add((new EmployeeWrapper(e)).Interface);
            }
            return employees;
        }

        

        public void UpdateEmployee(IProvideEmployeeData employeeData)
        {
            throw new NotImplementedException();
        }

        #endregion // Methods

    }
}
