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
    
    public partial class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] Pass { get; set; }
        public string Role { get; set; }
        public bool Enabled { get; set; }
        public bool Locked { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}