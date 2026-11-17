namespace Business_InfoTransito.Models.Events;

public class SinistroMapMarker
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public bool InjuredPeople { get; set; }
    public int SinistroType { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Road { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public List<int>? VehicleTypes { get; set; }
}
