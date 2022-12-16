namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface ICommentOperator
    {
        ICollection<Comment> GetToothComments(int patientId, string toothNumber);
        ICollection<Comment> GetPatientComments(int patientId);
    }
}
