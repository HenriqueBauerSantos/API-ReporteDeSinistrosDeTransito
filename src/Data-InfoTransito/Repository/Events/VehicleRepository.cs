using Business_InfoTransito.Interfaces.IRepositories.Events;
using Business_InfoTransito.Models.Events;
using Data_InfoTransito.Context;
using Microsoft.EntityFrameworkCore;

namespace Data_InfoTransito.Repository.Events;

public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(InfoTransitoDbContext db) : base(db) { }

    public async Task<Vehicle> GetVehicleBySinistro(Guid sinistroId)
    {
        return await Db.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SinistroId == sinistroId);
    }
}
