namespace DentalClinic.Windows
{
    using DentalClinic.XmlData;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface ISmsService
    {
        void SendMessage(ISms sms);
        event EventHandler<ConnectionChangedEventArgs> SmsServiceMalfunction;
        event EventHandler<ConnectionChangedEventArgs> SmsServiceOnLine;
    }
}
