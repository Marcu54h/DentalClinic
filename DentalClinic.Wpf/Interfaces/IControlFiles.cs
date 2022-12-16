namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IControlFiles
    {
        bool NewPatient { get; }
        IProvidePatientData ChoosenPatient { get; }
        void Refresh(bool RefreshMainCollection = false);
    }
}
