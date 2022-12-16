namespace DentalClinic.Wpf
{

    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IDisplayPriceList
    {
        void Refresh();
        bool AddSubGroup { get; }
        object SelectedItem { get; }

    }
}
