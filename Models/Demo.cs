//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RCMMaps.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Demo
    {
        public int ID { get; set; }
        public Nullable<int> BoardID { get; set; }
        public string Demo1 { get; set; }
        public Nullable<int> DemoWeeklyImpressions { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string Source { get; set; }
    
        public virtual Board Board { get; set; }
    }
}
