namespace DentalClinic.Wpf
{


    using DentalClinic.Data;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IControllOffices
    {
        ICollection<IOfficeData> OfficeCollection { get; }
    }
}
