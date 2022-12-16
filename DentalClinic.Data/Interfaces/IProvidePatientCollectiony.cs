namespace DentalClinic.Data
{

    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IProvidePatientCollection
    {
        ICollection<IProvidePatientData> Patients { get; }
    }
}
