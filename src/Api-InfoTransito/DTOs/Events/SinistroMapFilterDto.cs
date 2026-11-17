namespace Api_InfoTransito.DTOs.Events;

public class SinistroMapFilterDto
{
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public bool? InjuredPeople { get; set; }
    public int? SinistroType { get; set; }
    public int? RoadPavementType { get; set; }
    public int? RoadType { get; set; }
    public int? GroundType { get; set; }
    public int? Weather { get; set; }
    public int? TypeVehicle { get; set; }
    public double? MinLatitude { get; set; }
    public double? MaxLatitude { get; set; }
    public double? MinLongitude { get; set; }
    public double? MaxLongitude { get; set; }
}
