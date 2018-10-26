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
    
    public partial class Map
    {
        public Map()
        {
            this.Pins = new HashSet<Pin>();
            this.MapStyles = new HashSet<MapStyle>();
            this.Map_Boards = new HashSet<Map_Boards>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<double> CenterOnLat { get; set; }
        public Nullable<double> CenterOnLong { get; set; }
        public Nullable<int> ZoomLevel { get; set; }
        public bool DisplayZoomControl { get; set; }
        public bool DisplayMapTypeControl { get; set; }
        public bool DisplayScaleControl { get; set; }
        public bool DisplayStreetViewControl { get; set; }
        public bool DisplayPanControl { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public string Tags { get; set; }
    
        public virtual ICollection<Pin> Pins { get; set; }
        public virtual ICollection<MapStyle> MapStyles { get; set; }
        public virtual ICollection<Map_Boards> Map_Boards { get; set; }
    }
}
