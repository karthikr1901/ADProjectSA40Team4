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
    
    public partial class DisplayLowLevelStock_View
    {
        public string ItemID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Nullable<int> Balance { get; set; }
        public Nullable<int> Balance_after_reorder { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderQuantity { get; set; }
        public Nullable<int> SuggestedQuantity { get; set; }
        public string UnitOfMeasurement { get; set; }
    }
}
