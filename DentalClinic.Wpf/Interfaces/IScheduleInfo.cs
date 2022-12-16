namespace DentalClinic.Wpf
{

    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IScheduleInfo
    {
        IScheduleDay SelectedScheduleDay { get; }
        void RefreshSchedule();
    }
}
