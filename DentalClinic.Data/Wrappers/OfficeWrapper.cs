namespace DentalClinic.Data
{

    using System;

    /// <summary>
    /// 
    /// </summary>
    public class OfficeWrapper : IOfficeData
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string Label { get; set; }

        public string Caption { get { return Number + " - " + Label; } }

        public OfficeWrapper() { }

        public OfficeWrapper(Office office)
        {
            if (!(office is null))
            {
                Id = office.Id;
                Label = office.Label;
                Number = office.Number;
                Type = office.Type;
            }
           
        }

        public IOfficeData Interface => this;
    }
}
