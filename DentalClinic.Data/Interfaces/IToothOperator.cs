namespace DentalClinic.Data
{

    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IToothOperator
    {
        void RemoveToothComment(Tooth tooth, Comment comment);
        void CommentTooth(Tooth tooth, Comment comment);
        void AddToothToVisit(IVisitData visitData, Tooth tooth);
        Tooth GetToothInfo(Tooth tooth);
    }
}
