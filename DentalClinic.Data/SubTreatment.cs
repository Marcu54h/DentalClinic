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
    
    public partial class SubTreatment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubTreatment()
        {
            this.Sub2Treatment = new HashSet<Sub2Treatment>();
        }
    
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Nullable<int> TreatmentId { get; set; }
    
        public virtual Treatment Treatment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sub2Treatment> Sub2Treatment { get; set; }
    }
}
