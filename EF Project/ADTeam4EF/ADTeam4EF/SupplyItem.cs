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
    
    public partial class SupplyItem
    {
        public int SupplierID { get; set; }
        public string ItemID { get; set; }
        public decimal Price { get; set; }
        public int Priority { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}