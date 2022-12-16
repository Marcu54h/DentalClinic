namespace DentalClinic.Data
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class PatientOp : IPerformPatientOperation
    {

    #region Fields

    #endregion // Fields

    #region Properties



    #endregion // Properties

    #region Constructors

    #endregion // Constructors

    #region Methods

    public void AddPatient(IProvidePatientData patientData)
    {
        using (PDContainer pd = new PDContainer())
        {
            // First search person with given data
            Person person = new Person();

            pd.People.Add(person);
            person.AddDate = DateTime.UtcNow;

            person.Title = patientData.Title;
            person.FirstName = patientData.FirstName;
            person.LastName = patientData.LastName;
            person.PersonalNumber = patientData.PersonalNumber;
            person.ModifiedDate = DateTime.UtcNow;

            person.Patient = new Patient();

            person.Patient.Disabled = patientData.Disabled;
            person.Patient.DisabilityType = patientData.DisabilityType;
            person.Patient.Insured = patientData.Disabled;
            person.Patient.InsuranceNumber = patientData.InsuranceNumber;
            person.Patient.EmployeeId = patientData.EmployeeId;

            person.Patient.AddDate = person.ModifiedDate;
            person.Patient.ModifiedDate = person.ModifiedDate;

            pd.SaveChanges();
        }
    }

        public void AddPatientWithAddresses(IProvidePatientData patientData, ICollection<IProvideAddressData> addresses)
        {
            using (PDContainer pd = new PDContainer())
            {
                Person person = pd.People.Where(p => p.FirstName == patientData.FirstName && p.LastName == p.LastName).FirstOrDefault();

                bool newPerson = true;

                if (person == null)
                {
                    person = new Person
                    {
                        Title = patientData.Title,
                        FirstName = patientData.FirstName,
                        LastName = patientData.LastName,
                        PersonalNumber = patientData.PersonalNumber,
                        AddDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                }
                else newPerson = false;

                foreach (IProvideAddressData addressData in addresses)
                {
                    person.Addresses.Add(new Address
                    {
                        AddressType = addressData.AddressType,
                        City = addressData.City,
                        Street = addressData.Street,
                        HouseNumber = addressData.HouseNumber,
                        FlatNumber = addressData.FlatNumber,
                        StateProvince = addressData.StateProvince,
                        CountryRegion = addressData.CountryRegion,
                        PostalCode = addressData.PostalCode,
                        ModifiedDate = person.ModifiedDate,
                        Email = addressData.Email,
                        HomePhone = addressData.HomePhone,
                        WorkPhone = addressData.WorkPhone,
                        CellPhone = addressData.CellPhone
                    });
                }

                if (person.Patient == null)
                {
                    person.Patient = new Patient
                    {
                        Insured = patientData.Insured,
                        InsuranceNumber = patientData.InsuranceNumber,
                        Disabled = patientData.Disabled,
                        DisabilityType = patientData.DisabilityType,
                        EmployeeId = patientData.EmployeeId,
                        AddDate = person.AddDate,
                        ModifiedDate = person.ModifiedDate
                    };
                }
                
                
                if (newPerson)
                    pd.People.Add(person);

                pd.SaveChanges();
            }
        }

        public void DeletePatient(IProvidePatientData patientData)
        {
            using (PDContainer pd = new PDContainer())
            {
                Patient patient = pd.Patients
                                    .Include("Visits")
                                    .Include("Visits.Teeth")
                                    .Include("Comments")
                                    .Where(p => p.Id == patientData.Id).FirstOrDefault();

                if (!(patient is null))
                {
                    pd.Visits.Where(x => x.PatientId == patient.Id).ToList().ForEach(y =>
                    {
                        pd.Teeth.Where(z => z.VisitId == y.Id).ToList().ForEach(l =>
                        {
                            l.Treatments.Where(j => j.ToothId == l.Id).ToList().ForEach(q => pd.Treatments.Remove(q));
                            pd.Teeth.Remove(l);
                        });
                        pd.Visits.Remove(y);
                    });

                    pd.Comments.Where(x => x.PatientId == patient.Id).ToList().ForEach(y => pd.Comments.Remove(y));
                    pd.Patients.Remove(patient);
                    pd.SaveChanges();
                }
            }
        }

        public ICollection<IProvidePatientData> GetPatientCollection()
        {
            ICollection<IProvidePatientData> patients = new Collection<IProvidePatientData>();
            using (PDContainer pd = new PDContainer())
            {
                foreach (Patient p in pd.Patients.Include("Person"))
                    patients.Add((new PatientWrapper(p)).Interface);
            }
            return patients;
        }

        public void UpdatePatient(IProvidePatientData patientData)
        {
            using (PDContainer pd = new PDContainer())
            {
                Person person = pd.People.Where(x => x.Id == patientData.PersonId).FirstOrDefault();
                Patient patient = pd.Patients.Where(x => x.Id == patientData.Id).FirstOrDefault();

                if (!(person is null))
                {
                    person.Title = patientData.Title;
                    person.FirstName = patientData.FirstName;
                    person.LastName = patientData.LastName;
                    person.PersonalNumber = patientData.PersonalNumber;
                }
                if (!(patient is null))
                {
                    patient.Insured = patientData.Insured;
                    patient.InsuranceNumber = patientData.InsuranceNumber;
                    patient.Disabled = patientData.Disabled;
                    patient.DisabilityType = patientData.DisabilityType;
                    patient.EmployeeId = patientData.EmployeeId;
                }
                pd.SaveChanges();
            }
        }

        #endregion // Methods

    }
}
