using Api_InfoTransito.DTOs.People;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.Location;

public class PersonAddressDto : AddressDto
{
    [ScaffoldColumn(false)]
    public Guid personId { get; set; }
}
