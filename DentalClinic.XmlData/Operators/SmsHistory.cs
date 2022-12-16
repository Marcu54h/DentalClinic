namespace DentalClinic.XmlData
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// 
    /// </summary>
    public class SmsHistory : ISmsHistory
    {
        public ICollection<ISms> Smses { get; set; } = new Collection<ISms>();

        public void ItIsHistory(ISms sms)
        {
            if (!Smses.Contains(sms))
            {
                Smses.Add(sms);
            }
        }
    }
}
