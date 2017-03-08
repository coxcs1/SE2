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
    
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            this.Donations = new HashSet<Donation>();
            this.ServiceRequesteds = new HashSet<ServiceRequested>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Nullable<long> NewWalkIn { get; set; }
        public string DateArrived { get; set; }
        public Nullable<long> TelephoneAfterHrs { get; set; }
        public Nullable<long> PrankCall { get; set; }
        public Nullable<long> Email { get; set; }
        public Nullable<long> OutgoingCallMailEmail { get; set; }
        public Nullable<long> OffSite { get; set; }
        public Nullable<long> RepresentedBySomeoneElse { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation> Donations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequested> ServiceRequesteds { get; set; }
    }
}
