using Api_InfoTransito.DTOs.Events;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.Location;

public class SinistroAddressDto : AddressDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    [ScaffoldColumn(false)]
    public Guid SinistroId { get; set; }
}
