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
    
    public partial class RequestDetail
    {
        public int RequestDetailID { get; set; }
        public Nullable<int> RequestID { get; set; }
        public string RequestedItem { get; set; }
        public Nullable<int> RequestedQty { get; set; }
        public Nullable<int> ReceivedQty { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Request Request { get; set; }
    }
}