using Business_InfoTransito.Interfaces.IRepositories.Events;
using Business_InfoTransito.Models.Events;
using Data_InfoTransito.Context;

namespace Data_InfoTransito.Repository.Events;

public class SinistroExcludeSolicitationRepository : Repository<SinistroExcludeSolicitation>, ISinistroExcludeSolicitationRepository
{
    public SinistroExcludeSolicitationRepository(InfoTransitoDbContext db) : base(db) { }

    public async Task<IEnumerable<SinistroExcludeSolicitation>> AllPeopleBySinistro(Guid sinistroId)
    {
        return await Find(s => s.SinistroId == sinistroId);
    }
}
