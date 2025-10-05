using Business_InfoTransito.Enums;

namespace Business_InfoTransito.Models.Events;

public class Vehicle : Entity
{
    public Guid SinistroId { get; set; }
    public string? CarLisencePlate { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTime? ManufacturingYear { get; set; }
    public string? Color { get; set; }
    public TypeVehicle TypeVehicle { get; set; }
    public Guid? PersonId { get; set; }

    //EF
    public Sinistro Sinistro { get; set; }
}
