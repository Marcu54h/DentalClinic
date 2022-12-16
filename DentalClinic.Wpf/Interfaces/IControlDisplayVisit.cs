namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IControlDisplayVisit
    {
        IVisitData SelectedVisit { get; set; }
        WorkInterval SelectedWorkInterval { get; set; }
    }
}
