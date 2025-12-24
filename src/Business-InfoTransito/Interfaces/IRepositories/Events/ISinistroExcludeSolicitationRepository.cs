using Business_InfoTransito.Models.Events;

namespace Business_InfoTransito.Interfaces.IRepositories.Events;

public interface ISinistroExcludeSolicitationRepository : IRepository<SinistroExcludeSolicitation>
{
    Task<IEnumerable<SinistroExcludeSolicitation>> AllPeopleBySinistro(Guid sinistroId);
}
