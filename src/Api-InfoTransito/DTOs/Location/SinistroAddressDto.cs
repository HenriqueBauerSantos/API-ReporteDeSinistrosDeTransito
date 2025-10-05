using Api_InfoTransito.DTOs.Events;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.Location;

public class SinistroAddressDto : AddressDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Latitude")]
    public double Latitude { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Longitude")]
    public double Longitude { get; set; }

    [ScaffoldColumn(false)]
    public Guid SinistroId { get; set; }
}
