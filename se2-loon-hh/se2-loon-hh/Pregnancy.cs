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
    
    public partial class Pregnancy
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public string DueDate { get; set; }
        public Nullable<long> OBCareBeforeReg { get; set; }
        public Nullable<long> OBCareAfterReg { get; set; }
        public string HealthyDelivery { get; set; }
        public string BirthComplications { get; set; }
        public Nullable<long> ContinuedWithMotherhoodProgram { get; set; }
        public Nullable<long> LifeBridge { get; set; }
        public Nullable<long> FSS { get; set; }
        public Nullable<long> BetterWay { get; set; }
        public Nullable<long> CompleteBetterWay { get; set; }
        public Nullable<long> FamPlanningSession { get; set; }
        public Nullable<long> OasisClass { get; set; }
        public Nullable<long> HRCclass { get; set; }
        public string Comment { get; set; }
    
        public virtual Client Client { get; set; }
    }
}
