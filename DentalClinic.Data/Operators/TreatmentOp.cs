namespace DentalClinic.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class TreatmentOp : IPerformTreatmentOperation
    {
         
        public ICollection<Treatment> Treatments
        {
            get
            {
                using (PDContainer pd = new PDContainer())
                {
                    return pd.Treatments
                             .Include("SubTreatment")
                             .Include("SubTreatment.Sub2Treatment")
                             .Where(x => x.VisitId == null && x.ToothId == null)
                             .ToArray();
                }
            }
        }


        public void Add(Treatment treatment)
        {
            using (PDContainer pd = new PDContainer())
            {
                if (pd.Treatments.Where(x => x.Type == treatment.Type).FirstOrDefault() is null)
                    pd.Treatments.Add(treatment);
                pd.SaveChanges();
            }
        }

        public void Add(SubTreatment subTreatment, int treatmentId)
        {
            using (PDContainer pd = new PDContainer())
            {
                Treatment tempT = pd.Treatments.Include("SubTreatment").Where(x => x.Id == treatmentId).FirstOrDefault();

                if (!(tempT is null))
                    if (tempT.SubTreatment.Where(x => x.Type == subTreatment.Type).FirstOrDefault() is null)
                        tempT.SubTreatment.Add(subTreatment);

                pd.SaveChanges();
            }
        }

        public void Add(Sub2Treatment sub2Treatment, int subTreatmentId)
        {
            using (PDContainer pd = new PDContainer())
            {
                SubTreatment tempS = pd.SubTreatments.Include("Sub2Treatment").Where(x => x.Id == subTreatmentId).FirstOrDefault();

                if (!(tempS is null))
                    if (tempS.Sub2Treatment.Where(x => x.Type == sub2Treatment.Type).FirstOrDefault() is null)
                        tempS.Sub2Treatment.Add(sub2Treatment);

                pd.SaveChanges();
            }
        }

        public void Delete(Treatment treatment)
        {
            using (PDContainer pd = new PDContainer())
            {
                try
                {
                    Treatment tempT = pd.Treatments.Include("SubTreatment").Include("SubTreatment.Sub2Treatment").Where(x => x.Id == treatment.Id).FirstOrDefault();

                    if (!(tempT is null))
                        pd.Treatments.Remove(tempT);

                    pd.SubTreatments.Where(x => x.Treatment.Id == treatment.Id).ToList().ForEach(y =>
                    {
                        pd.SubTreatments.Remove(y);

                        pd.Sub2Treatments
                          .Where(x => x.SubTreatment.Id == y.Id)
                          .ToList()
                          .ForEach(z => pd.Sub2Treatments.Remove(z));

                    });

                    pd.SaveChanges();
                }
                catch { }
            }
        }

        public void Delete(SubTreatment subTreatment)
        {
            using (PDContainer pd = new PDContainer())
            {
                try
                {
                    pd.SubTreatments.Where(x => x.Id == subTreatment.Id).ToList().ForEach(y => pd.SubTreatments.Remove(y));

                    pd.Sub2Treatments
                      .Where(x => x.SubTreatment.Id == subTreatment.Id)
                      .ToList()
                      .ForEach(y => pd.Sub2Treatments.Remove(y));

                    pd.SaveChanges();
                }
                catch { }
            }
        }

        public void Delete(Sub2Treatment sub2Treatment)
        {
            using (PDContainer pd = new PDContainer())
            {
                try
                {
                    pd.Sub2Treatments.Where(x => x.Id == sub2Treatment.Id)
                      .ToList()
                      .ForEach(y => pd.Sub2Treatments.Remove(y));

                    pd.SaveChanges();
                }
                catch { }
            }
        }

        public void Update(Treatment treatment)
        {
            throw new System.NotImplementedException();
        }

        public void Update(SubTreatment subTreatment, int treatmentId)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Sub2Treatment sub2Treatment, int subTreatmentId)
        {
            throw new System.NotImplementedException();
        }
    }
}
