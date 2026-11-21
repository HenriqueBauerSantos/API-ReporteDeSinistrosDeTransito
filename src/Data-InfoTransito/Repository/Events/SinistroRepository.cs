using Business_InfoTransito.Enums;
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

    public async Task<IEnumerable<Sinistro>> GetByUserId(string userId)
    {
        return await Db.Sinistros
            .AsNoTracking()
            .Include(s => s.SinistroAddress)
            .Include(s => s.PeopleEnvolved)
            .Include(s => s.VehiclesEnvolved)
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<SinistroMapMarker>> GetByMapFilter(SinistroMapFilter filter)
    {
        if (filter == null)
            filter = new SinistroMapFilter();

        var query = Db.Sinistros
            .AsNoTracking()
            .Include(s => s.SinistroAddress)
            .Include(s => s.VehiclesEnvolved)
            .AsQueryable();

        if (filter.DateStart.HasValue)
            query = query.Where(s => s.Date >= filter.DateStart.Value);

        if (filter.DateEnd.HasValue)
            query = query.Where(s => s.Date <= filter.DateEnd.Value);

        if (filter.InjuredPeople.HasValue)
            query = query.Where(s => s.InjuredPeople == filter.InjuredPeople.Value);

        if (filter.SinistroType.HasValue)
            query = query.Where(s => s.SinistroType == (SinistroType)filter.SinistroType.Value);

        if (filter.RoadPavementType.HasValue)
            query = query.Where(s => s.RoadPavementType == (RoadPavementType)filter.RoadPavementType.Value);

        if (filter.RoadType.HasValue)
            query = query.Where(s => s.RoadType == (RoadType)filter.RoadType.Value);

        if (filter.GroundType.HasValue)
            query = query.Where(s => s.GroundType == (GroundType)filter.GroundType.Value);

        if (filter.Weather.HasValue)
            query = query.Where(s => s.Weather == (Weather)filter.Weather.Value);

        if (filter.MinLatitude.HasValue)
            query = query.Where(s => s.SinistroAddress != null && s.SinistroAddress.Latitude >= filter.MinLatitude.Value);

        if (filter.MaxLatitude.HasValue)
            query = query.Where(s => s.SinistroAddress != null && s.SinistroAddress.Latitude <= filter.MaxLatitude.Value);

        if (filter.MinLongitude.HasValue)
            query = query.Where(s => s.SinistroAddress != null && s.SinistroAddress.Longitude >= filter.MinLongitude.Value);

        if (filter.MaxLongitude.HasValue)
            query = query.Where(s => s.SinistroAddress != null && s.SinistroAddress.Longitude <= filter.MaxLongitude.Value);

        if (filter.TypeVehicle.HasValue)
            query = query.Where(s => s.VehiclesEnvolved.Any(v => v.TypeVehicle == (TypeVehicle)filter.TypeVehicle.Value));

        var sinistros = await query
            .Where(s => s.SinistroAddress != null)
            .ToListAsync();

        return sinistros.Select(s => new SinistroMapMarker
        {
            Id = s.Id,
            Date = s.Date,
            InjuredPeople = s.InjuredPeople,
            SinistroType = (int)s.SinistroType,
            Latitude = s.SinistroAddress!.Latitude,
            Longitude = s.SinistroAddress.Longitude,
            Road = s.SinistroAddress.Road,
            District = s.SinistroAddress.District,
            City = s.SinistroAddress.City,
            State = s.SinistroAddress.State,
            VehicleTypes = s.VehiclesEnvolved?
                .Select(v => (int)v.TypeVehicle)
                .Distinct()
                .ToList() ?? new List<int>()
        }).ToList();
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

    public async Task<IEnumerable<Sinistro>> GetSinistrosComSolicitacaoCompleto()
    {
        return await Db.Sinistros
            .AsNoTracking()
            .Include(s => s.SinistroAddress)
            .Include(s => s.PeopleEnvolved)
            .Include(s => s.VehiclesEnvolved)
            .Where(s => Db.SinistroExcludeSolicitation
                .Any(sol =>
                        sol.SinistroId == s.Id &&
                        sol.Status == ExclusionStatus.Solicitado))
            .ToListAsync();
    }
}
