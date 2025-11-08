using Business_InfoTransito.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api_InfoTransito.DTOs.Events;

public class VehicleDto
{
    public Guid Id { get; set; }
    public string? CarLisencePlate { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTime? ManufacturingYear { get; set; }
    public string? Color { get; set; }
    public TypeVehicle TypeVehicle { get; set; }

    [ScaffoldColumn(false)]
    public Guid SinistroId { get; set; }

    [ScaffoldColumn(false)]
    public Guid? PersonId { get; set; }
}
