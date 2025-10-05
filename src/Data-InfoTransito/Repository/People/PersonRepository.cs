using Business_InfoTransito.Interfaces.IRepositories.People;
using Business_InfoTransito.Models.Location;
using Business_InfoTransito.Models.People;
using Data_InfoTransito.Context;
using Microsoft.EntityFrameworkCore;

namespace Data_InfoTransito.Repository.People;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(InfoTransitoDbContext db): base(db) { }

    public async Task<IEnumerable<Person>> AllPeopleBySinistro(Guid sinistroId)
    {
        return await Find(p => p.SinistroID == sinistroId);
    }

    public async Task<Person> GetPersonAddress(Guid personId)
    {
        return await Db.Persons
            .AsNoTracking()
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Id == personId);
    }
}
