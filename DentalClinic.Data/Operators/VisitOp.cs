namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class VisitOp : IVisit
    {
        #region Fields



        #endregion // Fields

        #region Properties

     

        #endregion // Properties

        #region Constructors



        #endregion // Constructors

        #region Methods

        public ICollection<IVisitData> Visits
        {
            get
            {
                ICollection<IVisitData> visits = new Collection<IVisitData>();
                using (PDContainer pd = new PDContainer())
                {
                    pd.Visits
                      .Include("Office")
                      .Include("Employee")
                      .Include("Patient")
                      .Include("Comments")
                      .ToList().ForEach(x => visits.Add(new VisitWrapper(x, x.Employee, x.Patient, x.Office).Interface));
                }
                return visits;
            }
            set => throw new NotImplementedException();
        }

        public void RemoveVisit(Visit visit)
        {
            using (PDContainer pd = new PDContainer())
            {
                Visit v = pd.Visits.Where(x => x.Id == visit.Id).First();

                if (!(v is null))
                {
                    pd.Teeth.Where(x => x.VisitId == v.Id).ToList().ForEach(x =>
                    {
                        pd.Comments.Where(y => y.ToothId == x.Id).ToList().ForEach(z => pd.Comments.Remove(z));
                        pd.Treatments.Where(y => y.ToothId == x.Id).ToList().ForEach(z => pd.Treatments.Remove(z));
                        pd.Teeth.Remove(x);
                    });
                    pd.Comments.Where(x => x.VisitId == v.Id).ToList().ForEach(y => pd.Comments.Remove(y));
                    pd.Treatments.Where(x => x.VisitId == v.Id).ToList().ForEach(y => pd.Treatments.Remove(y));
                }
                pd.Visits.Remove(v);
                pd.SaveChanges();
            }
        }

        public void SaveVisit(Visit visit)
        {
            using (PDContainer pd = new PDContainer())
            {
                int v = visit.Id;
                // Delete comments which belongs to given visit
                pd.Comments.Where(x => x.VisitId == visit.Id).ToList().ForEach(y => pd.Comments.Remove(y));
                // Delete 1'st Comments which belongs to Teeth which belongs to given visit and then delete Teeth
                pd.Teeth.Where(x => x.VisitId == visit.Id).ToList().ForEach(y =>
                {
                    pd.Comments.Where(z => z.ToothId == y.Id).ToList().ForEach(l => pd.Comments.Remove(l));
                    pd.Treatments.Where(t => t.ToothId == y.Id).ToList().ForEach(o => pd.Treatments.Remove(o));
                    pd.Teeth.Remove(y);
                });
                // Delete Treatments which belongs to given visit
                pd.Treatments.Where(x => x.VisitId == visit.Id).ToList().ForEach(y => pd.Treatments.Remove(y));
                // Set Treatment VisitId to given visit.id and add this Treatment to PDContainer
                visit.Treatments.ToList().ForEach(x =>
                {
                    x.Visit = null;
                    x.VisitId = visit.Id;
                    pd.Treatments.Add(x);
                });

                visit.Comments.ToList().ForEach(x =>
                {
                    x.Visit = null;
                    x.VisitId = visit.Id;
                    pd.Comments.Add(x);
                });

                visit.Teeth.ToList().ForEach(x =>
                {
                    x.Visit = null;
                    x.Comments.ToList().ForEach(y =>
                    {
                        y.ToothId = x.Id;
                        pd.Comments.Add(y);
                    });

                    x.VisitId = visit.Id;
                    pd.Teeth.Add(x);
                });

                //pd.Visits.Remove(pd.Visits.Where(x => x.Id == visit.Id).First());

                //pd.Visits.Add(visit);

                pd.SaveChanges();
            }
        }

        public void SaveVisit(IVisitData visitData, ICollection<Treatment> treatments, ICollection<Comment> comments, ICollection<Tooth> teeth)
        {
            using (PDContainer pd = new PDContainer())
            {
                Visit v = pd.Visits.Where(x => x.Id == visitData.Id).FirstOrDefault();

                v.Comments.Clear();
                v.Treatments.Clear();
                v.Teeth.Clear();

                treatments.ToList().ForEach(x => v.Treatments.Add(x));
  
                comments.ToList().ForEach(x => v.Comments.Add(x));

                teeth.ToList().ForEach(x => v.Teeth.Add(x));

                pd.SaveChanges();
            }
        }

        public void Schedule(IVisitData visitData)
        {
            using (PDContainer pd = new PDContainer())
            {
                Visit visit = new Visit
                {
                    Date = visitData.Date,

                    Patient = pd.Patients.Where(x => x.Id == visitData.PatientData.Id).FirstOrDefault(),

                    Employee = pd.Employees.Where(x => x.Id == visitData.EmployeeData.Id).FirstOrDefault(),

                    Office = pd.Offices.Where(x => x.Id == visitData.OfficeData.Id).FirstOrDefault()
                };

                pd.Visits.Add(visit);

                pd.SaveChanges();
            }
        }

        public void UnSchedule(IVisitData visitData)
        {
            using (PDContainer pd = new PDContainer())
            {
                Visit visit = pd.Visits.Where(x => x.Id == visitData.Id).FirstOrDefault();
                if (!(visit is null))
                {
                    pd.Teeth.Where(x => x.VisitId == visit.Id).ToList().ForEach(y =>
                    {
                        y.Treatments.ToList().ForEach(g => pd.Treatments.Remove(g));
                        y.Comments.ToList().ForEach(h => pd.Comments.Remove(h));
                    });

                    pd.Visits.Remove(visit);
                    pd.SaveChanges();
                }
            }
        }

        public void Update(IVisitData visitData)
        {
            throw new NotImplementedException();
        }

        public Visit GetVisit(int id)
        {
            using (PDContainer pd = new PDContainer())
            {
                return pd.Visits
                         .Include("Treatments")
                         .Include("Treatments.SubTreatment")
                         .Include("Treatments.SubTreatment.Sub2Treatment")
                         .Include("Comments")
                         .Include("Teeth")
                         .Include("Teeth.Comments")
                         .Include("Teeth.Treatments")
                         .Include("Teeth.Treatments.SubTreatment")
                         .Include("Teeth.Treatments.SubTreatment.Sub2Treatment")
                         .Include("Patient")
                         .Include("Patient.Person")
                         .Where(x => x.Id == id)
                         .FirstOrDefault();
            }
        }

        #endregion // Methods

    }
}
