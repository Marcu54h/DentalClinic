namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class VisitScanner : IGatherVisitInfo, IVisitInfo
    {
        #region Fields

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Tooth> Teeth { get; set; }
        public IProvidePatientData Patient { get; set; }
        public ICollection<Treatment> Treatments { get; set; }

        #endregion // Fields

        #region Properties

        #endregion // Properties

        #region Constructors

        #endregion // Constructors

        #region Methods

        public IVisitInfo ScanVisit(IVisitData visitData)
        {
            Teeth = null;
            Comments = null;
            Patient = null;

            using (PDContainer pd = new PDContainer())
            {
                Visit v = pd.Visits
                            .Include(x => x.Teeth)
                            .Include(x => x.Comments)
                            .Include(x => x.Patient)
                            .Include(x => x.Treatments)
                            .Where(x => x.Id == visitData.Id)
                            .FirstOrDefault();

                try { Patient = new PatientWrapper(v.Patient).Interface; } catch { }
                try { Teeth = v.Teeth; } catch { }
                try { Comments = v.Comments; } catch { }
            }
            return this;
        }

        #endregion // Methods

    }
}
