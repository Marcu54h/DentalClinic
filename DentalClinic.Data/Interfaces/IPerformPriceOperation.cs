namespace DentalClinic.Data
{


    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IPerformPriceOperation
    {
        ICollection<PriceList> PriceLists { get; } 

        void AddPriceList(PriceList priceList);

        void AddGroupToPriceList(Group group, PriceList priceList);

        void AddSubGroupToGroup(SubGroup subGroup, Group group);

        void AddSub2GroupToSubGroup(Sub2Group sub2Group, SubGroup subGroup);

        void Delete(PriceList price);

        void Delete(Group group);

        void Delete(SubGroup subGroup);

        void Delete(Sub2Group sub2Group);
    }
}
