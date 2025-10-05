using Api_InfoTransito.DTOs.Location;
using Api_InfoTransito.DTOs.People;
using Business_InfoTransito.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.Events;

public class SinistroDto
{
    [Key]
    public Guid Id { get; set; }

    [ScaffoldColumn(false)]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Pessoas feridas")]
    public bool InjuredPeople { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Tipo de sinistro")]
    public int SinistroType { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Tipo de pavimento da via")]
    public int RoadPavementType { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Tipo de via")]
    public int RoadType { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Tipo de solo")]
    public int GroundType { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Condição do clima")]
    public int Weather { get; set; }

    public List<PersonDto>? PeopleEnvolved { get; set; }

    public List<VehicleDto>? VehiclesEnvolved { get; set; }

    public SinistroAddressDto? SinistroAddress { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Descrição do sinistro")]
    public string? SinistroDescription { get; set; }
}
