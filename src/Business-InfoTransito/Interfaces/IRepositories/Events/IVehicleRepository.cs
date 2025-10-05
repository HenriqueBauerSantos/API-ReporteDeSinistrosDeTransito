using Business_InfoTransito.Models.Events;

namespace Business_InfoTransito.Interfaces.IRepositories.Events;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle> GetVehicleBySinistro(Guid sinistroId);
}
