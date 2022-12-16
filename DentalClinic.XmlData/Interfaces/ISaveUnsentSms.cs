namespace DentalClinic.XmlData
{

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface ISaveUnsentSms
    {
        void ThisSmsNotSent(ISms sms);
        ICollection<ISms> PersonNeedNotify(int personId);
    }
}
