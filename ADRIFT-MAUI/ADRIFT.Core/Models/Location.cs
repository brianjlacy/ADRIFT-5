namespace ADRIFT.Core.Models;

public class Location : AdriftItem
{
    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public bool HideOnMap { get; set; }

    public List<Direction> Directions { get; set; } = new();
    public Dictionary<string, string> Properties { get; set; } = new();

    public override string DisplayName => ShortDescription;
    public override string ItemType => "Location";
}

public class Direction
{
    public string DirectionName { get; set; } = string.Empty;
    public string DestinationKey { get; set; } = string.Empty;
    public string? RestrictionDescription { get; set; }
    public bool HasRestriction => !string.IsNullOrEmpty(RestrictionDescription);
}
