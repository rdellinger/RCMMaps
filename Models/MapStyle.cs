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
    
    public partial class MapStyle
    {
        public int ID { get; set; }
        public Nullable<int> FeatureTypeID { get; set; }
        public string Hue { get; set; }
        public string Saturation { get; set; }
        public string Lightness { get; set; }
        public string Gamma { get; set; }
        public Nullable<int> MapID { get; set; }
    
        public virtual Map Map { get; set; }
        public virtual FeatureType FeatureType { get; set; }
    }
}
