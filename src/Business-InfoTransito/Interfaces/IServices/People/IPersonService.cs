using Business_InfoTransito.Models.Location;
using Business_InfoTransito.Models.People;

namespace Business_InfoTransito.Interfaces.IServices.People;

public interface IPersonService : IDisposable
{
    Task Add(Person person);
    Task Update(Person person);
    Task Delete(Person person);
    Task UpdateAddress(PersonAddress address);
}
