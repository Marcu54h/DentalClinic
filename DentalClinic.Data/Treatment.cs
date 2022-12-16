//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DentalClinic.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Treatment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Treatment()
        {
            this.SubTreatment = new HashSet<SubTreatment>();
        }
    
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Nullable<int> VisitId { get; set; }
        public Nullable<int> ToothId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubTreatment> SubTreatment { get; set; }
        public virtual Visit Visit { get; set; }
        public virtual Tooth Tooth { get; set; }
    }
}
