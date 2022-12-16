namespace DentalClinic.XmlData
{

    using System;

    public class Sms : ISms
    {
        private Guid identifier = Guid.Empty;

        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;
        public int PersonId { get; set; }
        public string Phone { get; set; }
        public Guid Identifier
        {
            get
            {
                identifier = identifier == Guid.Empty ? Guid.NewGuid() : identifier;
                return identifier;
            }
            set
            {
                identifier = identifier == Guid.Empty ? value : identifier;
            }
        }

        public bool Sended { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime VisitDateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int VisitId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
