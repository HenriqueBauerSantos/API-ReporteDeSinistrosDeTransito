using Business_InfoTransito.Models.Location;

namespace Business_InfoTransito.Interfaces.IRepositories.Location;

public interface IPersonAddressRepository : IRepository<PersonAddress>
{
    Task<PersonAddress> GetByPersonId(Guid personId);
}
