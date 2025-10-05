using Business_InfoTransito.Models.Events;

namespace Business_InfoTransito.Interfaces.IServices.Events;

public interface IVehicleService : IDisposable
{
    Task Add(Vehicle vehicle);
    Task Update(Vehicle vehicle);
    Task Delete(Guid id);
}
