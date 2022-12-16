namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class ToothOp : IToothOperator
    {
        #region Fields



        #endregion // Fields

        #region Properties

        #endregion // Properties

        #region Constructors



        #endregion // Constructors

        #region Methods

        public void AddToothToVisit(IVisitData visitData, Tooth tooth)
        {
            using (PDContainer pd = new PDContainer())
            {
                Visit v = pd.Visits.Find(visitData.Id);

                if (!(v is null))
                {
                    v.Teeth.Add(tooth);
                }
            }
        }

        public void CommentTooth(Tooth tooth, Comment comment)
        {
            using (PDContainer pd = new PDContainer())
            {
                Tooth t = pd.Teeth.Find(tooth.Id);

                if (!(t is null))
                {
                    //t.
                }
            }
        }

        public void RemoveToothComment(Tooth tooth, Comment comment)
        {
            throw new NotImplementedException();
        }

        public Tooth GetToothInfo(Tooth tooth)
        {
            using (PDContainer pd = new PDContainer())
            {
                return pd.Teeth.Include("Treatments")
                    .Include("Comments")
                    .Where(x => x.Id == tooth.Id).FirstOrDefault();
            }
        }

        #endregion // Methods



    }
}
