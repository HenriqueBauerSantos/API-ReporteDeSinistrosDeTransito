using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.Location;

public class AddressDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Rua")]
    public string? Road { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Número")]
    public string? Number { get; set; }

    [DisplayName("Complemento")]
    public string? Complement { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "O campo {0} precisa ter {1} caracteres.")]
    public string? Cep { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Bairro")]
    public string? District { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Cidade")]
    public string? City { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Estado")]
    public string? State { get; set; }
}
