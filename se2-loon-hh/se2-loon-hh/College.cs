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
    
    public partial class College
    {
        public long Id { get; set; }
        public Nullable<long> ClientId { get; set; }
        public string Location { get; set; }
        public string Year { get; set; }
        public string YearStarted { get; set; }
        public string Status { get; set; }
        public string YearCompleted { get; set; }
        public string DegreePursuing { get; set; }
    
        public virtual Client Client { get; set; }
    }
}