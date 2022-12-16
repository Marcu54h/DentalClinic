using System;

namespace DentalClinic.XmlData
{
    public interface ISms
    {
        bool Sended { get; set; }
        Guid Identifier { get; set; }
        string Content { get; set; }
        DateTime DateTime { get; set; }
        DateTime VisitDateTime { get; set; }
        int PersonId { get; set; }
        int VisitId { get; set; }
        string Phone { get; set; }
    }
}