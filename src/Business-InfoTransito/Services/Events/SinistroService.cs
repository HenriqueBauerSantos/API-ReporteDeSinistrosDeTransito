using Business_InfoTransito.Interfaces;
using Business_InfoTransito.Interfaces.IRepositories.Events;
using Business_InfoTransito.Interfaces.IRepositories.Location;
using Business_InfoTransito.Interfaces.IServices.Events;
using Business_InfoTransito.Interfaces.IServices.People;
using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Enums;
using Business_InfoTransito.Models.Validations.Events;
using Business_InfoTransito.Models.Validations.Location;
using Business_InfoTransito.Models.Validations.People;

namespace Business_InfoTransito.Services.Events;

public class SinistroService : BaseService, ISinistroService
{
    private readonly ISinistroRepository _sinistroRepository;
    private readonly IPersonService _personService;
    private readonly IVehicleService _vehicleService;
    private readonly ISinistroAddressRepository _sinistroAddressRepository;
    private readonly ISinistroExcludeSolicitationRepository _sinistroExcludeSolicitationRepository;

    public SinistroService(IPersonService personService,
        ISinistroRepository sinistroRepository,
        IVehicleService vehicleService,
        ISinistroAddressRepository sinistroAddressRepository,
        ISinistroExcludeSolicitationRepository excludeSolicitationRepository,
        INotifier notifier) : base(notifier)
    {
        _personService = personService;
        _vehicleService = vehicleService;
        _sinistroRepository = sinistroRepository;
        _sinistroAddressRepository = sinistroAddressRepository;
        _sinistroExcludeSolicitationRepository = excludeSolicitationRepository;
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

    public async Task Update(Sinistro sinistroReceived)
    {
        if (!ExecuteValidation(new SinistroValidation(), sinistroReceived)) return;

        foreach (var item in sinistroReceived.VehiclesEnvolved)
        {
            if (!ExecuteValidation(new VehicleValidation(), item)) return;
        }
        foreach (var item in sinistroReceived.PeopleEnvolved)
        {
            if (!ExecuteValidation(new PersonValidation(), item)) return;
        }

        var newVehicles = sinistroReceived.VehiclesEnvolved.ToList();
        var newPeople = sinistroReceived.PeopleEnvolved.ToList();

        var sinistroToUpdate = await _sinistroRepository.GetSinistroAllData(sinistroReceived.Id);
        var oldVehicles = sinistroToUpdate.VehiclesEnvolved?.ToList();
        var oldPeople = sinistroToUpdate.PeopleEnvolved?.ToList();

        sinistroToUpdate.Date = sinistroReceived.Date;
        sinistroToUpdate.InjuredPeople = sinistroReceived.InjuredPeople;
        sinistroToUpdate.SinistroType = sinistroReceived.SinistroType;
        sinistroToUpdate.RoadPavementType = sinistroReceived.RoadPavementType;
        sinistroToUpdate.RoadType = sinistroReceived.RoadType;
        sinistroToUpdate.GroundType = sinistroReceived.GroundType;
        sinistroToUpdate.Weather = sinistroReceived.Weather;
        sinistroToUpdate.SinistroAddress = sinistroReceived.SinistroAddress;
        sinistroToUpdate.SinistroDescription = sinistroReceived.SinistroDescription;

        //Logica validar veículos pessoas

        await _sinistroRepository.Update(sinistroToUpdate);
    }

    public async Task UpdateSinistroAddress(Sinistro sinistroReceived)
    {
        if (!ExecuteValidation(new SinistroAddressValidation(), sinistroReceived.SinistroAddress)) return;

        var sinistroAddressToUpdate = await _sinistroAddressRepository.GetBySinistroId(sinistroReceived.Id);

        sinistroAddressToUpdate.Road = sinistroReceived.SinistroAddress?.Road;
        sinistroAddressToUpdate.Number = sinistroReceived.SinistroAddress?.Number;
        sinistroAddressToUpdate.Complement = sinistroReceived.SinistroAddress?.Complement;
        sinistroAddressToUpdate.Cep = sinistroReceived.SinistroAddress?.Cep;
        sinistroAddressToUpdate.District = sinistroReceived.SinistroAddress?.District;
        sinistroAddressToUpdate.City = sinistroReceived.SinistroAddress?.City;
        sinistroAddressToUpdate.State = sinistroReceived.SinistroAddress?.State;
        sinistroAddressToUpdate.Latitude = sinistroReceived.SinistroAddress.Latitude;
        sinistroAddressToUpdate.Longitude = sinistroReceived.SinistroAddress.Longitude;

        await _sinistroAddressRepository.Update(sinistroAddressToUpdate);
    }

    public async Task DeleteSolicitation(Guid id, Guid userId)
    {
        var sinistro = await _sinistroRepository.GetById(id);

        var solicitation = new SinistroExcludeSolicitation
        {
            SinistroId = sinistro.Id,
            Motive = "Erro de cadastro",
            RequestDate = DateTime.Now,
            Status = Enums.ExclusionStatus.Solicitado,
            AnalyzedByUserId = userId,
            AnalyzedDate = DateTime.Now,
        };

        await _sinistroExcludeSolicitationRepository.Add(solicitation);
    }

    public async Task Delete(Guid id, Guid userId)
    {
        var sinistro = await _sinistroRepository.GetSinistroAllData(id);

        var solicitacoes = await _sinistroExcludeSolicitationRepository
            .Find(s => s.SinistroId == id);

        if (solicitacoes.Any())
        {
            foreach (var item in solicitacoes)
            {
                item.AnalyzedDate = DateTime.Now;
                item.AnalyzedByUserId = userId;
                item.Status = ExclusionStatus.Aprovado;
                item.SinistroId = null;
                await _sinistroExcludeSolicitationRepository.Update(item);
            }
        }

        if (sinistro.PeopleEnvolved.Any())
        {
            foreach (var item in sinistro.PeopleEnvolved)
            {
                await _personService.Delete(item.Id);
            }
        }

        if (sinistro.VehiclesEnvolved.Any())
        {
            foreach (var item in sinistro.VehiclesEnvolved)
            {
                await _vehicleService.Delete(item.Id);
            }
        }

        await _sinistroAddressRepository.Delete(sinistro.SinistroAddress.Id);

        await _sinistroRepository.Delete(sinistro.Id);        
    }  
       

    public void Dispose()
    {
        _sinistroRepository.Dispose();
        _personService.Dispose();
        _vehicleService.Dispose();
    }    
}
