using Business_InfoTransito.Interfaces;
using Business_InfoTransito.Interfaces.IRepositories.Location;
using Business_InfoTransito.Interfaces.IRepositories.People;
using Business_InfoTransito.Interfaces.IServices.People;
using Business_InfoTransito.Models.Location;
using Business_InfoTransito.Models.People;

namespace Business_InfoTransito.Services.People;

public class PersonService : BaseService, IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IPersonAddressRepository _personAddressRepository;

    public PersonService(IPersonRepository personRepository,
        IPersonAddressRepository personAddressRepository,
        INotifier notifier) : base(notifier)
    {
        _personRepository = personRepository;
        _personAddressRepository = personAddressRepository;
    }

    public async Task Add(Person person)
    {
        await _personRepository.Add(person);
    }

    public async Task Delete(Person person)
    {
        throw new NotImplementedException();
    }    

    public async Task Update(Person person)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAddress(PersonAddress address)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _personAddressRepository.Dispose();
        _personRepository.Dispose();
    }
}
