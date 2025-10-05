using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.People;

namespace Business_InfoTransito.Interfaces.IRepositories.Events;

public interface ISinistroRepository : IRepository<Sinistro>
{
    Task<IEnumerable<Person>> GetAllPeople();
    Task<IEnumerable<Vehicle>> GetAllVehicles();
}
