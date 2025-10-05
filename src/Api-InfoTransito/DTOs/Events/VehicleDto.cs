using Business_InfoTransito.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.Events;

public class VehicleDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "O campo {0} precisa ter {1} caracteres.")]
    [DisplayName("Placa do veículo")]
    public string? CarLisencePlate { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Marca do veículo")]
    public string? Brand { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Modelo do veículo")]
    public string? Model { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Ano de fabricação")]
    public DateTime? ManufacturingYear { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    [DisplayName("Cor do veículo")]
    public string? Color { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Tipo de veículo")]
    public TypeVehicle TypeVehicle { get; set; }

    [ScaffoldColumn(false)]
    public Guid SinistroId { get; set; }

    [ScaffoldColumn(false)]
    public Guid? PersonId { get; set; }
}
