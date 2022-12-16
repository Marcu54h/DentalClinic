using System.Collections.Generic;

namespace DentalClinic.Data
{
    public interface IPerformEmployeeOperation
    {
        ICollection<IProvideEmployeeData> GetEmployeeCollection();
        void AddEmployee(IProvideEmployeeData employeeData);
        void DeleteEmployee(IProvideEmployeeData employeeData);
        IProvideEmployeeData GetEmployee(int? employeeId);
        void UpdateEmployee(IProvideEmployeeData employeeData);
    }
}
