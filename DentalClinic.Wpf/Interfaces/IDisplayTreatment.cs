namespace DentalClinic.Wpf
{

    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IDisplayTreatment
    {
        void Refresh();
        bool AddSubTreatment { get; }
        object SelectedItem { get; }

    }
}
