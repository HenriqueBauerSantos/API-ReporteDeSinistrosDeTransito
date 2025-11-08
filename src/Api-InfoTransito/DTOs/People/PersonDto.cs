using Api_InfoTransito.DTOs.Events;
using Api_InfoTransito.DTOs.Location;
using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.Location;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.People;

public class PersonDto
{

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? BirthDate { get; set; }
    public bool? Gender { get; set; }
    public string? CPF { get; set; }
    public string? RG { get; set; }
    public string? CNH { get; set; }
    public string? Phone { get; set; }
    public PersonAddressDto? Address { get; set; }
    public Guid SinistroId { get; set; }
}
