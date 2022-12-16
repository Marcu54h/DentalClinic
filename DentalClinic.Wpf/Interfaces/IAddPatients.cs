namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IAddPatients
    {
        IProvidePatientData Patient { get; set; }
    }
}
