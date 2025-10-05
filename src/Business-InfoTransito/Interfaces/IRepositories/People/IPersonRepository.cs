using Business_InfoTransito.Models.People;

namespace Business_InfoTransito.Interfaces.IRepositories.People;

public interface IPersonRepository : IRepository<Person>
{
    Task<Person> GetPersonAddress(Guid personId);
    Task<IEnumerable<Person>> AllPeopleBySinistro(Guid sinistroId);
}
