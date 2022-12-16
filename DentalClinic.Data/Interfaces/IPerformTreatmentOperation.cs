using System.Collections;
using System.Collections.Generic;

namespace DentalClinic.Data
{
    public interface IPerformTreatmentOperation
    {
        ICollection<Treatment> Treatments { get; }

        void Add(Treatment treatment);

        void Add(SubTreatment subTreatment, int treatmentId);

        void Add(Sub2Treatment sub2Treatment, int subTreatmentId);

        void Delete(Treatment treatment);

        void Delete(SubTreatment subTreatment);

        void Delete(Sub2Treatment sub2Treatment);

        void Update(Treatment treatment);

        void Update(SubTreatment subTreatment, int treatmentId);

        void Update(Sub2Treatment sub2Treatment, int subTreatmentId);
    }
}
