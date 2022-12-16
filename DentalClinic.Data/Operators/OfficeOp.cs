namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class OfficeOp : IOffice
    {
        #region Fields



        #endregion // Fields

        #region Properties

        public ICollection<IOfficeData> Offices
        {
            get
            {
                ICollection<IOfficeData> offices = new Collection<IOfficeData>();
                using (PDContainer pd = new PDContainer())
                {
                    pd.Offices.ToList().ForEach(x =>
                    {
                        if (!(x is null))
                            offices.Add(new OfficeWrapper(x).Interface);
                    });
                    return offices;
                }
            }
            set
            {
                using (PDContainer pd = new PDContainer())
                {

                    foreach(IOfficeData officeData in value)
                    {
                        if (!(officeData.Id is default(int)))
                        {
                            Office office = pd.Offices.Where(x => x.Id == officeData.Id).FirstOrDefault();

                            if (!(office is null))
                            {
                                office.Type = officeData.Type;
                                office.Label = officeData.Label;
                                office.Number = officeData.Number;
                                
                            }
                            else
                            {
                                pd.Offices.Add(new Office
                                {
                                    Type = officeData.Type,
                                    Label = officeData.Label,
                                    Number = officeData.Number
                                });
                            }
                        }
                        else
                        {
                            pd.Offices.Add(new Office
                            {
                                Type = officeData.Type,
                                Label = officeData.Label,
                                Number = officeData.Number
                            });
                        }
                    }
                    pd.SaveChanges();
                }
            }
        }

        #endregion // Properties

        #region Constructors


        #endregion // Constructors

        #region Methods

        public IOfficeData GetOffice(IOfficeData officeData)
        {
            using (PDContainer pd = new PDContainer())
            {
                return new OfficeWrapper(
                    pd.Offices.Where(x => x.Id == officeData.Id).FirstOrDefault()
                    ).Interface;
            }
        }

        public void RemoveOffice(IOfficeData officeData)
        {
            using (PDContainer pd = new PDContainer())
            {
                Office office = pd.Offices.Where(x => x.Id == officeData.Id).FirstOrDefault();
                try {
                    pd.Offices.Remove(office); pd.SaveChanges();
                } catch { }
            }
        }

        public void UpateOffice(IOfficeData officeData)
        {
            using (PDContainer pd = new PDContainer())
            {
                Office office = pd.Offices.Where(x => x.Id == officeData.Id).FirstOrDefault();
                if (!(office is null))
                {
                    office.Type = officeData.Type;
                    office.Label = officeData.Label;
                    office.Number = officeData.Number;
                    pd.SaveChanges();
                }
            }
        }

        #endregion // Methods



    }
}
