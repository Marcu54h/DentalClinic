namespace DentalClinic.Data
{

    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IProvideEmployeeCollection
    {
        ICollection<IProvideEmployeeData> Employees { get; }
    }
}
