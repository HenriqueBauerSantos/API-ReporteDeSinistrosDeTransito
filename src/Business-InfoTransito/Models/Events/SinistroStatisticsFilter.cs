namespace Business_InfoTransito.Models.Events;

public class SinistroStatisticsFilter
{
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public string? GroupBy { get; set; }
    public bool? InjuredPeople { get; set; }
    public int? SinistroType { get; set; }
    public int? RoadPavementType { get; set; }
    public int? RoadType { get; set; }
    public int? GroundType { get; set; }
    public int? Weather { get; set; }
    public int? TypeVehicle { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
}
