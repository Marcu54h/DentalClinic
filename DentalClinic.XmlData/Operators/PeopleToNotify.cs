namespace DentalClinic.XmlData
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DentalClinic.Data;

    /// <summary>
    /// 
    /// </summary>
    public class PeopleToNotify : IPeopleToNotify
    {
        public ICollection<Visit> VisitsToAnounce
        {
            get
            {
                ICollection<Visit> visits = new Collection<Visit>();

                MainDataContext.MainContext.Visits
                                           .Include("Patient.Person")
                                           .Include("Patient.Person.Addresses")
                                           .Where(x => (x.Date - DateTime.Now).TotalHours < 24 && (x.Date - DateTime.Now).TotalHours > 2)
                                           .ToList()
                                           .ForEach(y =>
                                           {
                                               if (!(y.Patient.Person.Addresses is null))
                                               {
                                                   if (!(y.Patient.Person.Addresses.First().CellPhone is null))
                                                   {
                                                       visits.Add(y);
                                                   }
                                               }
                                           });
                return visits;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<Visit> VisitsToAnounceAsync
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
