namespace DentalClinic.Data.Interfaces
{

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IClinicDay
    {
        DateTime Day { get; }
        ICollection<IVisitData> Visits { get; }
        int NumberOfVisits { get; }
        void GetClinicDay(DateTime date);
    }
}
