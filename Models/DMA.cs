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
    
    public partial class DMA
    {
        public DMA()
        {
            this.Boards = new HashSet<Board>();
        }
    
        public int ID { get; set; }
        public string DMA1 { get; set; }
    
        public virtual ICollection<Board> Boards { get; set; }
    }
}
