using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.Location;

namespace Business_InfoTransito.Models.People;

public class Person : Entity
{
    public Guid SinistroID { get; set; }
    public string? Name { get; set; }
    public string? BirthDate { get; set; }
    public bool? Gender { get; set; }
    public string? CPF { get; set; }
    public string? RG { get; set; }
    public string? CNH { get; set; }
    public string? Phone { get; set; }
    public PersonAddress? Address { get; set; }

    //EF
    public Sinistro Sinistro { get; set; }
}
