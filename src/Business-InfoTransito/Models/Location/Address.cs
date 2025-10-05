using System.ComponentModel.DataAnnotations.Schema;

namespace Business_InfoTransito.Models.Location;

[NotMapped]
public class Address : Entity
{
    public string? Road { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? Cep { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
}
