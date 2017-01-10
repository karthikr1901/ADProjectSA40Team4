//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ADTeam4EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Request()
        {
            this.RequestDetails = new HashSet<RequestDetail>();
        }
    
        public int RequestID { get; set; }
        public string RequestStatus { get; set; }
        public Nullable<int> RequestByEmployeeID { get; set; }
        public string RequestByDepartmentID { get; set; }
        public Nullable<int> ApprovedByEmployeeID { get; set; }
        public Nullable<int> ProcessedByEmployeeID { get; set; }
        public Nullable<int> ReceivedByEmployeeID { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public string Remark { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual Employee Employee2 { get; set; }
        public virtual Employee Employee3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }
    }
}