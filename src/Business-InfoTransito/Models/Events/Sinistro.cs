using Business_InfoTransito.Enums;
using Business_InfoTransito.Models.Location;
using Business_InfoTransito.Models.People;
using System;

namespace Business_InfoTransito.Models.Events;

public class Sinistro : Entity
{
    public DateTime Date { get; set; }
    public bool InjuredPeople { get; set; }
    public SinistroType SinistroType { get; set; }
    public RoadPavementType RoadPavementType { get; set; }
    public RoadType RoadType { get; set; }
    public GroundType GroundType { get; set; }
    public Weather Weather { get; set; }
    public List<Person>? PeopleEnvolved { get; set; }
    public List<Vehicle>? VehiclesEnvolved { get; set; }
    public SinistroAddress? SinistroAddress { get; set; }
    public string? SinistroDescription { get; set; }
}
