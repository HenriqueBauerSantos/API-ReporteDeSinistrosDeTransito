using Business_InfoTransito.Interfaces.IRepositories.Location;
using Business_InfoTransito.Models.Location;
using Data_InfoTransito.Context;
using Microsoft.EntityFrameworkCore;

namespace Data_InfoTransito.Repository.Location;

public class SinistroAddressRepository : Repository<SinistroAddress>, ISinistroAddressRepository
{
    public SinistroAddressRepository(InfoTransitoDbContext db) : base(db) { }

    public async Task<SinistroAddress> GetBySinistroId(Guid sinistroId)
    {
        return await Db.SinistrosAddresses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SinistroId == sinistroId);
    }
}
