//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace se2_loon_hh
{
    using System;
    using System.Collections.Generic;
    
    public partial class Donation
    {
        public long Id { get; set; }
        public Nullable<long> ServiceId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
    
        public virtual Service Service { get; set; }
    }
}