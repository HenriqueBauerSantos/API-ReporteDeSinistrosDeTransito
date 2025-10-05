using Business_InfoTransito.Interfaces;
using Business_InfoTransito.Interfaces.IRepositories.Events;
using Business_InfoTransito.Interfaces.IServices.Events;
using Business_InfoTransito.Models.Events;

namespace Business_InfoTransito.Services.Events;

public class VehicleService : BaseService, IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository,
        INotifier notifier) : base(notifier)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task Add(Vehicle vehicle)
    {
        await _vehicleRepository.Add(vehicle);
    }

    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }    

    public async Task Update(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _vehicleRepository.Dispose();
    }
}
