namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class PriceListOp : IPerformPriceOperation
    {


        #region Fields



        #endregion // Fields

        #region Properties



        #endregion // Properties

        #region Constructors



        #endregion // Constructors

        #region Methods

        public ICollection<PriceList> PriceLists
        {
            get
            {

                    return MainDataContext.MainContext.PriceLists
                             .Include("Groups")
                             .Include("Groups.SubGroups")
                             .Include("Groups.SubGroups.Sub2Groups")
                             .ToArray();

            }
        }
        
        public void AddGroupToPriceList(Group group, PriceList priceList)
        {
            MainDataContext.MainContext.PriceLists.Where(x => x.Id == priceList.Id).First()
                                       .Groups.Add(group);

            MainDataContext.MainContext.SaveChanges();
        }

        public void AddPriceList(PriceList priceList)
        {
            MainDataContext.MainContext.PriceLists.Add(priceList);

            MainDataContext.MainContext.SaveChanges();
        }

        public void AddSub2GroupToSubGroup(Sub2Group sub2Group, SubGroup subGroup)
        {

            MainDataContext.MainContext.SubGroups.Where(x => x.Id == subGroup.Id).First().Sub2Groups.Add(sub2Group);

            MainDataContext.MainContext.SaveChanges();

        }

        public void AddSubGroupToGroup(SubGroup subGroup, Group group)
        {

            MainDataContext.MainContext.Groups.Where(x => x.Id == group.Id).First().SubGroups.Add(subGroup);

            MainDataContext.MainContext.SaveChanges();
        }

        public void Delete(PriceList price)
        {
            throw new NotImplementedException();
        }

        public void Delete(Group group)
        {
            throw new NotImplementedException();
        }

        public void Delete(SubGroup subGroup)
        {
            throw new NotImplementedException();
        }

        public void Delete(Sub2Group sub2Group)
        {
            throw new NotImplementedException();
        }

        #endregion // Methods

    }
}
