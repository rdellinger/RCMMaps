using System;
using System.ComponentModel.DataAnnotations;

namespace RCMMaps.Models
{

    [MetadataType(typeof(BoardMetadata))]
    public partial class Board
    {
    }

    [MetadataType(typeof(BoardImageMetadata))]
    public partial class BoardImage
    {
    }

    [MetadataType(typeof(BoardRatingMetadata))]
    public partial class BoardRating
    {
    }

    [MetadataType(typeof(BoardTypeMetadata))]
    public partial class BoardType
    {
    }

    [MetadataType(typeof(BoardVendorMetadata))]
    public partial class BoardVendor
    {
    }

    [MetadataType(typeof(DemoMetadata))]
    public partial class Demo
    {
    }

    [MetadataType(typeof(DMAMetadata))]
    public partial class DMA
    {
    }

    [MetadataType(typeof(FeatureTypeMetadata))]
    public partial class FeatureType
    {
    }

    [MetadataType(typeof(MapMetadata))]
    public partial class Map
    {
    }

    [MetadataType(typeof(Map_BoardsMetadata))]
    public partial class Map_Boards
    {
    }

    [MetadataType(typeof(MapDefaultMetadata))]
    public partial class MapDefault
    {
    }

    [MetadataType(typeof(MapStyleMetadata))]
    public partial class MapStyle
    {
    }

    [MetadataType(typeof(MapStyleDefaultMetadata))]
    public partial class MapStyleDefault
    {
    }

    [MetadataType(typeof(PinMetadata))]
    public partial class Pin
    {
    }

    [MetadataType(typeof(PinTypeMetadata))]
    public partial class PinType
    {
    }

    [MetadataType(typeof(ProductionVendorMetadata))]
    public partial class ProductionVendor
    {
    }

}