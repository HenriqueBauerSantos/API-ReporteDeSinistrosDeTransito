using Business_InfoTransito.Interfaces.IRepositories.Location;
using Business_InfoTransito.Models.Location;
using Data_InfoTransito.Context;
using Microsoft.EntityFrameworkCore;

namespace Data_InfoTransito.Repository.Location;

public class PersonAddressRepository : Repository<PersonAddress>, IPersonAddressRepository
{
    public PersonAddressRepository(InfoTransitoDbContext db) : base(db){ }

    public async Task<PersonAddress> GetByPersonId(Guid personId)
    {
        return await Db.PersonsAddresses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PersonId == personId);
    }
}
