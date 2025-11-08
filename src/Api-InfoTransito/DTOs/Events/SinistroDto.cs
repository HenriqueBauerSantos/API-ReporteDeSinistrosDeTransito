using Api_InfoTransito.DTOs.Location;
using Api_InfoTransito.DTOs.People;
using Business_InfoTransito.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.Events;

public class SinistroDto
{
    public Guid Id { get; set; }

    [ScaffoldColumn(false)]
    public DateTime Date { get; set; }
    public bool InjuredPeople { get; set; }
    public int SinistroType { get; set; }
    public int RoadPavementType { get; set; }
    public int RoadType { get; set; }
    public int GroundType { get; set; }
    public int Weather { get; set; }
    public List<PersonDto>? PeopleEnvolved { get; set; }
    public List<VehicleDto>? VehiclesEnvolved { get; set; }
    public SinistroAddressDto? SinistroAddress { get; set; }
    public string? SinistroDescription { get; set; }
}
