using Api_InfoTransito.DTOs.Events;
using Api_InfoTransito.DTOs.Location;
using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.Location;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.People;

public class PersonDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Nome")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Data de nascimento")]
    public string? BirthDate { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public bool? Gender { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo {0} precisa ter {1} caracteres.")]
    public string? CPF { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(9, MinimumLength = 9, ErrorMessage = "O campo {0} precisa ter {1} caracteres.")]
    public string? RG { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "O campo {0} precisa ter {1} caracteres.")]
    public string? CNH { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Telefone")]
    public string? Phone { get; set; }

    public PersonAddressDto? Address { get; set; }

    [ScaffoldColumn(false)]
    public Guid SinistroId { get; set; }
}
