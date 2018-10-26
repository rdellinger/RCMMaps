using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RCMMaps.Models
{
    
    public class BoardMetadata
    {
        [Display(Name = "Board Type")]
        public Nullable<int> BoardTypeID;

        [Display(Name = "Board Vendor")]
        public Nullable<int> BoardVendorID;

        [Display(Name = "DMA")]
        public Nullable<int> DMAID;

        [Display(Name = "Production Vendor")]
        public Nullable<int> ProductionVendorID;

        [Display(Name = "Board #")]
        [Required(ErrorMessage = "Board Number is required.")]
        public string BoardNumber;

        [Display(Name = "Description")]
        public string Description;

        [Display(Name = "Address")]
        public string Address;

        [Display(Name = "City")]
        public string City;

        [Display(Name = "Zip")]
        public string Zip;

        [Display(Name = "Latitude")]
        public Nullable<double> Latitude;

        [Display(Name = "Longitude")]
        public Nullable<double> Longitude;

        [Display(Name = "Direction Facing")]
        public string Facing;

        [Display(Name = "Side of Street")]
        public string SideOfStreet;

        [Display(Name = "Read Direction")]
        public string ReadDirection;

        [Display(Name = "Illuminated?")]
        public bool Illuminated;

        [Display(Name = "Board Height (ft)")]
        public string BoardHeight;

        [Display(Name = "Board Width (ft)")]
        public string BoardWidth;

        [Display(Name = "Weekly 18+ Impressions")]
        public Nullable<int> Weekly18PlusImpressions;

        [Display(Name = "Tab Panel ID")]
        public Nullable<int> TabPanelID;

        [Display(Name = "Link to Production Specs (URL)")]
        [Url(ErrorMessage = "Enter a valid URL, beginning with http://")]
        public string LinkToProductionSpecs;

        [Display(Name = "Board Rating")]
        [Range(1, 5, ErrorMessage = "Enter a number between 1 and 5.")]
        public Nullable<int> BoardRatingID;

        [Display(Name = "Notes")]
        public string Notes;

        [Display(Name = "Tags")]
        public string Tags;

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateCreated;

        [Display(Name = "Created By")]
        public string CreatedBy;

        [Display(Name = "Date Modified")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateModified;

        [Display(Name = "Modified By")]
        public string ModifiedBy;

    }

    public class BoardImageMetadata
    {
        [Display(Name = "Board")]
        public Nullable<int> BoardID;

        [Display(Name = "Title")]
        public string Title;

        [Display(Name = "Image")]
        public string Image;

        [Display(Name = "Image Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ImageDate;

        [Display(Name = "Display Order")]
        public Nullable<int> DisplayOrder;
    }

    public class BoardRatingMetadata
    {
        [Display(Name = "Rating #")]
        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Enter a number between 1 and 5.")]
        public Nullable<int> Rating;

        [Display(Name = "Rating")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description;
    }
    
    public class BoardTypeMetadata
    {
        [Display(Name = "Board Type")]
        [Required(ErrorMessage = "Board Type is required.")]
        public string BoardType1;
    }

    public class BoardVendorMetadata
    {
        [Display(Name = "Vendor")]
        [Required(ErrorMessage = "Vendor is required.")]
        public string Vendor;
    }

    public class DemoMetadata
    {
        [Display(Name = "Board")]
        [Required(ErrorMessage = "Board is required.")]
        public Nullable<int> BoardID;

        [Display(Name = "Demo")]
        [Required(ErrorMessage = "Demo is required.")]
        public string Demo1;

        [Display(Name = "Demo Weekly Impressions")]
        public Nullable<int> DemoWeeklyImpressions;

        [Display(Name = "Date Updated")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateUpdated;

        [Display(Name = "Source")]
        public string Source;
    }

    public class DMAMetadata
    {
        [Display(Name = "DMA")]
        [Required(ErrorMessage = "DMA is required.")]
        public string DMA1;
    }
    
    public class FeatureTypeMetadata
    {
        [Display(Name = "Feature")]
        [Required(ErrorMessage = "Feature is required.")]
        public string FeatureType1;

        [Display(Name = "Google Code")]
        [Required(ErrorMessage = "Google Code is required.")]
        public string Code;
    }

    public class MapMetadata
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title;

        [Display(Name = "Description")]
        public string Description;

        [Display(Name = "Width")]
        public Nullable<int> Width;

        [Display(Name = "Height")]
        public Nullable<int> Height;

        [Display(Name = "Center Map on Latitude")]
        [Required(ErrorMessage = "A latitude for centering the map is required.")]
        public Nullable<double> CenterOnLat;

        [Display(Name = "Center Map on Longitude")]
        [Required(ErrorMessage = "A longitude for centering the map is required.")]
        public Nullable<double> CenterOnLong;

        [Display(Name = "Zoom Level")]
        public Nullable<int> ZoomLevel;

        [Display(Name = "Display Zoom Control?")]
        public bool DisplayZoomControl;

        [Display(Name = "Display Map Type Control?")]
        public bool DisplayMapTypeControl;

        [Display(Name = "Display Scale Control?")]
        public bool DisplayScaleControl;

        [Display(Name = "Display Street View Control?")]
        public bool DisplayStreetViewControl;

        [Display(Name = "Display Pan Control?")]
        public bool DisplayPanControl;

        [Display(Name = "Tags")]
        public string Tags;

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateCreated;

        [Display(Name = "Created By")]
        public string CreatedBy;

        [Display(Name = "Date Modified")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateModified;

        [Display(Name = "Modified By")]
        public string ModifiedBy;
    }

    public class Map_BoardsMetadata
    {
        [Display(Name = "Map")]
        [Required(ErrorMessage = "Map is required.")]
        public Nullable<int> MapID;

        [Display(Name = "Board")]
        [Required(ErrorMessage = "Board is required.")]
        public Nullable<int> BoardID;

        [Display(Name = "Pin Type")]
        [Required(ErrorMessage = "Pin Type is required.")]
        public Nullable<int> PinTypeID;
    }

    public class MapDefaultMetadata
    {
        [Display(Name = "Width")]
        [Required(ErrorMessage = "Width is required.")]
        public Nullable<int> Width;

        [Display(Name = "Height")]
        [Required(ErrorMessage = "Height is required.")]
        public Nullable<int> Height;

        [Display(Name = "Center Map on Latitude")]
        [Required(ErrorMessage = "A latitude for centering the map is required.")]
        public Nullable<double> CenterOnLat;

        [Display(Name = "Center Map on Longitude")]
        [Required(ErrorMessage = "A longitude for centering the map is required.")]
        public Nullable<double> CenterOnLong;

        [Display(Name = "Zoom Level")]
        [Required(ErrorMessage = "Zoom Level is required.")]
        public Nullable<int> ZoomLevel;

        [Display(Name = "Display Zoom Control?")]
        public bool DisplayZoomControl;

        [Display(Name = "Display Map Type Control?")]
        public bool DisplayMapTypeControl;

        [Display(Name = "Display Scale Control?")]
        public bool DisplayScaleControl;

        [Display(Name = "Display Street View Control?")]
        public bool DisplayStreetViewControl;

        [Display(Name = "Display Pan Control?")]
        public bool DisplayPanControl;
    }

    public class MapStyleMetadata
    {
        [Display(Name = "Feature")]
        [Required(ErrorMessage = "Feature is required.")]
        public Nullable<int> FeatureTypeID;

        [Display(Name = "Hue")]
        [Required(ErrorMessage = "Hue is required.")]
        public string Hue;

        [Display(Name = "Saturation")]
        [Required(ErrorMessage = "Saturation is required.")]
        public string Saturation;

        [Display(Name = "Lightness")]
        [Required(ErrorMessage = "Lightness is required.")]
        public string Lightness;

        [Display(Name = "Gamma")]
        [Required(ErrorMessage = "Gamma is required.")]
        public string Gamma;

        [Display(Name = "Map")]
        public Nullable<int> MapID;   
    }

    public class MapStyleDefaultMetadata
    {
        [Display(Name = "Feature")]
        [Required(ErrorMessage = "Feature is required.")]
        public Nullable<int> FeatureTypeID;

        [Display(Name = "Hue")]
        [Required(ErrorMessage = "Hue is required.")]
        public string Hue;

        [Display(Name = "Saturation")]
        [Required(ErrorMessage = "Saturation is required.")]
        public string Saturation;

        [Display(Name = "Lightness")]
        [Required(ErrorMessage = "Lightness is required.")]
        public string Lightness;

        [Display(Name = "Gamma")]
        [Required(ErrorMessage = "Gamma is required.")]
        public string Gamma;
    }

    public class PinMetadata
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title;

        [Display(Name = "Latitude")]
        [Required(ErrorMessage = "Latitude is required.")]
        public Nullable<double> Latitude;

        [Display(Name = "Longitude")]
        [Required(ErrorMessage = "Longitude is required.")]
        public Nullable<double> Longitude;

        [Display(Name = "Pin Type")]
        [Required(ErrorMessage = "Pin Type is required.")]
        public Nullable<int> PinTypeID;

        [Display(Name = "Map")]
        [Required(ErrorMessage = "Map is required.")]
        public Nullable<int> MapID;   
    }

    public class PinTypeMetadata
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title;

        [Display(Name = "Image")]
        public string Image;
    }

    public class ProductionVendorMetadata
    {
        [Display(Name = "Production Vendor")]
        [Required(ErrorMessage = "Production Vendor is required.")]
        public string Vendor;
    }
}