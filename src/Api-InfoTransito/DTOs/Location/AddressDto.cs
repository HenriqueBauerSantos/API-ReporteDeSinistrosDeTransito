using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.Location;

public class AddressDto
{
    public Guid Id { get; set; }
    public string? Road { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? Cep { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
}
