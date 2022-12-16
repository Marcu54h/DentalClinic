namespace DentalClinic.Wpf
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using DentalClinic.Data;
    using DentalClinic.Windows;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class SmsNotifier : ISmsNotify
    {
        #region Fields

        public List<(int, int)> PersonAgreements = new List<(int, int)>();

        #endregion // Fields

        #region Properties

        #endregion // Properties

        #region Constructors

        #endregion // Constructors

        #region Methods

        public void NotifyThisOne(int personId, int addressId)
        {
            throw new NotImplementedException();
        }

        public int? ShouldINotifyThisPerson(int personId)
        {
            foreach((int, int) tuple in PersonAgreements)
            {
                if (tuple.Item1 == personId)
                    return tuple.Item2;
            }
            return null;
        }

        public void ThisOneAgreed(int personId, int addressId)
        {
            foreach((int, int) tuple in PersonAgreements)
            {
                if (tuple.Item1 == personId)
                {
                    if (tuple.Item2 != addressId)
                    {
                        PersonAgreements.Remove(tuple);
                        PersonAgreements.Add((personId, addressId));
                        return;
                    }
                    return;
                }
            }
            PersonAgreements.Add((personId, addressId));
         }

        public void ThisOneResigned(int personId)
        {
            foreach ((int, int) tuple in PersonAgreements)
            {
                if (tuple.Item1 == personId)
                {
                    PersonAgreements.Remove(tuple);
                    return;
                }
            }
        }

        public void SaveToXml()
        {
            try
            {
                XmlHelper.ToXmlFile(this, App.container.Resolve<Container>().SmsNotifyAgreementFile);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while saving patients sms agreements: " + e);
            }

        }

        public void LoadFromXml()
        {
            try
            {
                if (new FileInfo(App.container.Resolve<Container>().SmsNotifyAgreementFile).Exists)
                {
                    PersonAgreements =
                        XmlHelper.FromXmlFile<SmsNotifier>(App.container.Resolve<Container>().SmsNotifyAgreementFile).PersonAgreements;
                }
                else
                {
                    SaveToXml();

                    LoadFromXml();
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong: " + e);
            }
            
        }


        #endregion // Methods

    }
}
