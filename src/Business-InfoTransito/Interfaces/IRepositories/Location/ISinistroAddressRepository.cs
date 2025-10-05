using Business_InfoTransito.Models.Location;

namespace Business_InfoTransito.Interfaces.IRepositories.Location;

public interface ISinistroAddressRepository : IRepository<SinistroAddress>
{
    Task<SinistroAddress> GetBySinistroId(Guid sinistroId);
}
