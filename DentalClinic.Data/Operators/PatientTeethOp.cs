namespace DentalClinic.Data
{ 

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class PatientTeethOp : ICommentOperator
    {
        #region Fields

        private ICollection<Comment> comments = new Collection<Comment>();

        private ICollection<Tooth> patientTeeth = new Collection<Tooth>();

        private ICollection<Visit> patientVisits = new Collection<Visit>();

        private IDictionary<string, ICollection<Comment>> teethComments = new Dictionary<string, ICollection<Comment>>();

        private int patientId;

        #endregion // Fields

        #region Properties



        #endregion // Properties

        #region Constructors

        public PatientTeethOp()
        {

        }

        #endregion // Constructors

        #region Methods

        public void AddComment(Visit visit, Tooth tooth, Comment comment)
        {
            if (patientVisits.Contains(visit))
            {
                Visit v = patientVisits.ElementAt(patientVisits.ToList().IndexOf(visit));
                //if (v.Teeth.Contains(tooth))
            }
        }

        public void RemoveComment(Comment comment)
        {
            bool commentRemoved = false;
            foreach(Tooth tooth in patientTeeth)
            {
                if (tooth.Comments.Contains(comment))
                {
                    tooth.Comments.Remove(comment);
                    commentRemoved = true;
                }
                if (commentRemoved)
                {
                    using (PDContainer pd = new PDContainer())
                    {
                        pd.SaveChanges();
                    }
                    commentRemoved = false;
                }
            }
        }

        public void Refresh(int patientId)
        {
            this.patientId = patientId;

            comments.Clear();

            patientTeeth.Clear();

            patientVisits.Clear();

            teethComments.Clear();

            collectPatientVisits();

            collectPatientTeeth();

            collectTeethComments();
        }

        private void collectPatientVisits()
        {
           MainDataContext.MainContext.Visits.Where(x => x.PatientId == patientId)
                                      .Include(y => y.Comments)
                                      .Include(z => z.Teeth)
                                      .Include("Teeth.Comments")
                                      .AsNoTracking()
                                      .ToList().ForEach(x => patientVisits.Add(x));

        }

        private void collectPatientTeeth()
        {
            foreach(Visit v in patientVisits)
            {
                if (v.Teeth.Count > 0)
                {
                    foreach(Tooth t in v.Teeth)
                    {
                        patientTeeth.Add(t);
                    }
                }
            }
        }

        private void collectTeethComments()
        {
            foreach (Tooth tooth in patientTeeth)
            {
                if (teethComments.Keys.Contains(tooth.Number))
                {
                    if (tooth.Comments.Count > 0)
                    {
                        foreach (Comment comment in tooth.Comments)
                            teethComments[tooth.Number].Add(comment);
                    }
                }
                else
                {
                    if (tooth.Comments.Count > 0)
                    {
                        teethComments.Add(tooth.Number, new Collection<Comment>());
                        foreach (Comment comment in tooth.Comments)
                            teethComments[tooth.Number].Add(comment);
                    }
                }
            }
        }


        public ICollection<Comment> GetToothComments(int patientId, string toothNumber)
        {
            Refresh(patientId);

            if (teethComments.Keys.Contains(toothNumber))
            {
                foreach (Comment comment in teethComments[toothNumber])
                {
                    comments.Add(comment);
                }
            }
            

            return comments;
        }

        public ICollection<Comment> GetPatientComments(int patientId)
        {
            Refresh(patientId);

            foreach (Visit visit in patientVisits)
            {
                if (visit.Comments.Count > 0)
                {
                    foreach (Comment comment in visit.Comments)
                    {
                        comments.Add(comment);
                    }
                }

            }
            return comments;
        }

        #endregion // Methods
    }
}
