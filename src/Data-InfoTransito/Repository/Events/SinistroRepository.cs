using Business_InfoTransito.Interfaces.IRepositories.Events;
using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.People;
using Data_InfoTransito.Context;
using Microsoft.EntityFrameworkCore;

namespace Data_InfoTransito.Repository.Events;

public class SinistroRepository : Repository<Sinistro>, ISinistroRepository
{
    public SinistroRepository(InfoTransitoDbContext db) : base(db)
    {
    }

    public async Task<IEnumerable<Person>> GetAllPeople()
    {
        return await Db.Sinistros
            .AsNoTracking()
            .Include(x => x.PeopleEnvolved)
            .SelectMany(x => x.PeopleEnvolved)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetAllVehicles()
    {
        return await Db.Sinistros
            .AsNoTracking()
            .Include(x => x.VehiclesEnvolved)
            .SelectMany(x => x.VehiclesEnvolved)
            .ToListAsync();
    }

    public async Task<Sinistro> GetSinistroAllData(Guid id)
    {
        return await Db.Sinistros
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(x => x.SinistroAddress)
            .Include(x => x.PeopleEnvolved)
            .Include(x => x.VehiclesEnvolved)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
