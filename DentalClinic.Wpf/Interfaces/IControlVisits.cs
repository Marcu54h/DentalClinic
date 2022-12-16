namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IControlVisits
    {
        bool NewVisit { get; }
        IVisitData SelectedVisit { get; set; }
        void Refresh(bool RefreshMainCollection = false);
    }
}
