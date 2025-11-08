using Business_InfoTransito.Interfaces;
using Business_InfoTransito.Interfaces.IRepositories.Events;
using Business_InfoTransito.Interfaces.IServices.Events;
using Business_InfoTransito.Interfaces.IServices.People;
using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.Location;
using Business_InfoTransito.Models.Validations.Events;
using Business_InfoTransito.Models.Validations.Location;
using Business_InfoTransito.Models.Validations.People;

namespace Business_InfoTransito.Services.Events;

public class SinistroService : BaseService, ISinistroService
{
    private readonly ISinistroRepository _sinistroRepository;
    private readonly IPersonService _personService;
    private readonly IVehicleService _vehicleService;

    public SinistroService(IPersonService personService,
        ISinistroRepository sinistroRepository,
        IVehicleService vehicleService,
        INotifier notifier) : base(notifier)
    {
        _personService = personService;
        _vehicleService = vehicleService;
        _sinistroRepository = sinistroRepository;
    }

    public async Task Add(Sinistro sinistroReceived)
    {
        //Validations
        if (!ExecuteValidation(new SinistroValidation(), sinistroReceived)
            || !ExecuteValidation(new SinistroAddressValidation(), sinistroReceived.SinistroAddress)) return;

        
        foreach (var item in sinistroReceived.VehiclesEnvolved)
        {
            if (!ExecuteValidation(new VehicleValidation(), item)) return;
        }

        foreach (var item in sinistroReceived.PeopleEnvolved)
        {
            if (!ExecuteValidation(new PersonValidation(), item)) return;
        }

        //Logics to add
        var SinistroToAdd = new Sinistro
        {
            Date = sinistroReceived.Date,
            InjuredPeople = sinistroReceived.InjuredPeople,
            SinistroType = sinistroReceived.SinistroType,
            RoadPavementType = sinistroReceived.RoadPavementType,
            RoadType = sinistroReceived.RoadType,
            GroundType = sinistroReceived.GroundType,
            Weather = sinistroReceived.Weather,
            SinistroDescription = sinistroReceived.SinistroDescription,
            PeopleEnvolved = sinistroReceived.PeopleEnvolved,
            VehiclesEnvolved = sinistroReceived.VehiclesEnvolved,
            SinistroAddress = sinistroReceived.SinistroAddress,
        };

        SinistroToAdd.SinistroAddress.SinistroRegister = SinistroToAdd;
        SinistroToAdd.SinistroAddress.SinistroId = SinistroToAdd.Id;        
        
        foreach (var item in SinistroToAdd.VehiclesEnvolved)
        {
            item.SinistroId = SinistroToAdd.Id;
            item.Sinistro = SinistroToAdd;
        }
            
        foreach (var item in SinistroToAdd.PeopleEnvolved)
        {
            item.SinistroID = SinistroToAdd.Id;
            item.Sinistro = SinistroToAdd;
        }

        await _sinistroRepository.Add(SinistroToAdd);
    }

    public async Task Update(Sinistro sinistro)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }    

    public async Task UpdateSinistroAddress(SinistroAddress sinistroAddress)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _sinistroRepository.Dispose();
        _personService.Dispose();
        _vehicleService.Dispose();
    }
}
