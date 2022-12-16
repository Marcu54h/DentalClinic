namespace DentalClinic.Wpf
{

    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// 
    /// </summary>
    public class AppSettings
    {

        public string DataBaseIP { get; set; } = string.Empty;
        public string SMSServicePhoneIP { get; set; } = string.Empty;
        public int SMSServicePort { get; set; } = 8816;
        public string DataBaseName { get; set; } = string.Empty;
        public string DataBaseUser { get; set; } = string.Empty;
        public string DataBasePass { get; set; } = string.Empty;
        public string NameOfClinic { get; set; } = string.Empty;
        public string AddressOfClinic { get; set; } = string.Empty;
        public bool ThisIsMainHost { get; set; } = default;
        public int StartWorkHour { get; set; } = default;
        public int EndWorkHour { get; set; } = default;
        public int ScheduleInterval { get; set; } = default;
    }

    public class GuiSettings
    {
        public double AppWindowHeight { get; set; } = default;
        public double AppWindowWidth { get; set; } = default;
        public WindowState AppWindowState { get; set; } = WindowState.Normal;
        public Color UnfilledVisitsColor { get; set; } = Colors.Red;
    }
}
