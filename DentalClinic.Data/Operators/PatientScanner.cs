namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class PatientScanner : IGatherPatientInfo, IPatientInfo
    {
        #region Fields

        public Employee PatientEmployee { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Visit> Visits { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public PriceList PriceList { get; set; }

        #endregion // Fields

        #region Properties

        #endregion // Properties

        #region Constructors

        public PatientScanner()
        {
            
        }

        #endregion // Constructors

        #region Methods

        public IPatientInfo ScanPatient(IProvidePatientData patientData)
        {
            PatientEmployee = null;
            Addresses = null;
            Visits = null;
            Comments = null;
            PriceList = null;

            using (PDContainer pd = new PDContainer())
            {
                Person p = pd.People
                             .Include(x => x.Patient)
                             .Include(x => x.Employee)
                             .Include(x => x.Addresses)
                             .Include(x => x.Patient.Comments)
                             .Include(x => x.Patient.Visits)
                             .Include(x => x.Patient.PriceList)
                             .Where(x => x.Patient.Id == patientData.Id)
                             .FirstOrDefault();

                try { PatientEmployee = p.Employee; } catch { }
                try { Addresses = p.Addresses; } catch { }
                try { Visits = p.Patient.Visits; } catch { }
                try { Comments = p.Patient.Comments; } catch { }
                try { PriceList = p.Patient.PriceList; } catch { }
                  
            }
            return this;
        }

        #endregion // Methods

    }
}
