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
    
    public partial class PregnancyHistory
    {
        public long Id { get; set; }
        public Nullable<long> ClientId { get; set; }
        public Nullable<long> NumToTerm { get; set; }
        public Nullable<long> NumAdoption { get; set; }
        public Nullable<long> NumMiscarriage { get; set; }
        public Nullable<long> NumAbortion { get; set; }
    
        public virtual Client Client { get; set; }
    }
}