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
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.AddressChanges = new HashSet<AddressChange>();
            this.Applications = new HashSet<Application>();
            this.Children = new HashSet<Child>();
            this.ClassAttendances = new HashSet<ClassAttendance>();
            this.ClientFathers = new HashSet<ClientFather>();
            this.ClientProgramCurriculums = new HashSet<ClientProgramCurriculum>();
            this.Colleges = new HashSet<College>();
            this.Employments = new HashSet<Employment>();
            this.GEDeducations = new HashSet<GEDeducation>();
            this.HighSchools = new HashSet<HighSchool>();
            this.OtherEducations = new HashSet<OtherEducation>();
            this.Pregnancies = new HashSet<Pregnancy>();
            this.PregnancyHistories = new HashSet<PregnancyHistory>();
            this.ServiceRequesteds = new HashSet<ServiceRequested>();
        }
    
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameComment { get; set; }
        public string StreetAddr1 { get; set; }
        public string StreetAddr2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public Nullable<long> IsNew { get; set; }
        public string Status { get; set; }
        public string MaritalStatus { get; set; }
        public string BirthDate { get; set; }
        public string Ethnicity { get; set; }
        public string HousingLocation { get; set; }
        public Nullable<long> OnDisability { get; set; }
        public Nullable<long> ApplyingForDisability { get; set; }
        public Nullable<long> WillingToWork { get; set; }
        public long ProgressPoints { get; set; }
        public string CurrentStudentEnrollment { get; set; }
        public string EducationalBackground { get; set; }
        public Nullable<long> IsSmoker { get; set; }
        public Nullable<long> AttendClass { get; set; }
        public Nullable<long> BetterWay { get; set; }
        public Nullable<long> HRCclass { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddressChange> AddressChanges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application> Applications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Child> Children { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassAttendance> ClassAttendances { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientFather> ClientFathers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientProgramCurriculum> ClientProgramCurriculums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<College> Colleges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employment> Employments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GEDeducation> GEDeducations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HighSchool> HighSchools { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OtherEducation> OtherEducations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pregnancy> Pregnancies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PregnancyHistory> PregnancyHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequested> ServiceRequesteds { get; set; }
    }
}