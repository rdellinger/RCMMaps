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
    
    public partial class Map_Boards
    {
        public int ID { get; set; }
        public Nullable<int> MapID { get; set; }
        public Nullable<int> BoardID { get; set; }
        public Nullable<int> PinTypeID { get; set; }
    
        public virtual Board Board { get; set; }
        public virtual Map Map { get; set; }
        public virtual PinType PinType { get; set; }
    }
}
