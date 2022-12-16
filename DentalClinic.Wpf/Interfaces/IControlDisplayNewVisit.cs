namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IControlDisplayNewVisit
    {
        Visit SelectedVisit { get; set; }
        Office VisitsOffice { get; set; }
        Employee SelectedEmployee { get; set; }
        WorkInterval SelectedWorkInterval { get; set; }
        void Refresh();
    }
}
