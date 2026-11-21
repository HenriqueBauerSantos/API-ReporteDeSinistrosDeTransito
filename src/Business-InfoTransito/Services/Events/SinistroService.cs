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
            UserId = sinistroReceived.UserId,
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

    public async Task<SinistroStatistics> GetStatistics(SinistroStatisticsFilter filter)
    {
        filter ??= new SinistroStatisticsFilter();

        if (!ValidateStatisticsFilter(filter))
            return new SinistroStatistics();

        var groupBy = string.IsNullOrWhiteSpace(filter.GroupBy)
            ? "day"
            : filter.GroupBy.Trim().ToLowerInvariant();

        filter.GroupBy = groupBy;
        var accidents = (await _sinistroRepository.GetByStatisticsFilter(filter)).ToList();
        if (accidents.Count == 0)
            return new SinistroStatistics();

        var accidentsWithInjuries = accidents.Count(s => s.InjuredPeople);
        var statistics = new SinistroStatistics
        {
            TotalAccidents = accidents.Count,
            AccidentsWithInjuries = accidentsWithInjuries,
            AccidentsWithoutInjuries = accidents.Count - accidentsWithInjuries,
            InjuryRate = Math.Round((double)accidentsWithInjuries / accidents.Count * 100, 2),
            ByAccidentType = accidents
                .GroupBy(s => (int)s.SinistroType)
                .Select(g => new SinistroValueStatistic { Value = g.Key, Count = g.Count() })
                .OrderBy(x => x.Value)
                .ToList(),
            ByWeather = accidents
                .GroupBy(s => (int)s.Weather)
                .Select(g => new SinistroValueStatistic { Value = g.Key, Count = g.Count() })
                .OrderBy(x => x.Value)
                .ToList(),
            ByRoadType = accidents
                .GroupBy(s => (int)s.RoadType)
                .Select(g => new SinistroValueStatistic { Value = g.Key, Count = g.Count() })
                .OrderBy(x => x.Value)
                .ToList(),
            ByRoadPavementType = accidents
                .GroupBy(s => (int)s.RoadPavementType)
                .Select(g => new SinistroValueStatistic { Value = g.Key, Count = g.Count() })
                .OrderBy(x => x.Value)
                .ToList(),
            ByGroundType = accidents
                .GroupBy(s => (int)s.GroundType)
                .Select(g => new SinistroValueStatistic { Value = g.Key, Count = g.Count() })
                .OrderBy(x => x.Value)
                .ToList(),
            ByVehicleType = accidents
                .SelectMany(s => (s.VehiclesEnvolved ?? []).Select(v => new { AccidentId = s.Id, Value = (int)v.TypeVehicle }))
                .GroupBy(x => x.Value)
                .Select(g => new SinistroVehicleStatistic
                {
                    Value = g.Key,
                    AccidentCount = g.Select(x => x.AccidentId).Distinct().Count(),
                    VehicleCount = g.Count()
                })
                .OrderBy(x => x.Value)
                .ToList(),
            ByLocation = accidents
                .GroupBy(s => new { City = s.SinistroAddress?.City, District = s.SinistroAddress?.District })
                .Select(g => new SinistroLocationStatistic
                {
                    City = g.Key.City,
                    District = g.Key.District,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.City)
                .ThenBy(x => x.District)
                .ToList()
        };

        statistics.ByPeriod = BuildPeriodStatistics(accidents, filter, groupBy);
        return statistics;
    }

    private bool ValidateStatisticsFilter(SinistroStatisticsFilter filter)
    {
        var isValid = true;

        if (filter.DateStart.HasValue && filter.DateEnd.HasValue &&
            filter.DateStart.Value.Date > filter.DateEnd.Value.Date)
        {
            Notify("A data inicial não pode ser posterior à data final.");
            isValid = false;
        }

        if (!string.IsNullOrWhiteSpace(filter.GroupBy) &&
            !new[] { "day", "week", "month" }.Contains(filter.GroupBy.Trim(), StringComparer.OrdinalIgnoreCase))
        {
            Notify("O agrupamento deve ser 'day', 'week' ou 'month'.");
            isValid = false;
        }

        if (!IsValidEnum<SinistroType>(filter.SinistroType) ||
            !IsValidEnum<RoadPavementType>(filter.RoadPavementType) ||
            !IsValidEnum<RoadType>(filter.RoadType) ||
            !IsValidEnum<GroundType>(filter.GroundType) ||
            !IsValidEnum<Weather>(filter.Weather) ||
            !IsValidEnum<TypeVehicle>(filter.TypeVehicle))
        {
            Notify("Um ou mais filtros de enumeração possuem valor inválido.");
            isValid = false;
        }

        return isValid;
    }

    private static bool IsValidEnum<TEnum>(int? value) where TEnum : struct, Enum =>
        !value.HasValue || Enum.IsDefined(typeof(TEnum), value.Value);

    private static List<SinistroPeriodStatistic> BuildPeriodStatistics(
        List<Sinistro> accidents,
        SinistroStatisticsFilter filter,
        string groupBy)
    {
        var firstDate = filter.DateStart.HasValue
            ? DateOnly.FromDateTime(filter.DateStart.Value)
            : accidents.Min(s => DateOnly.FromDateTime(s.Date));
        var lastDate = filter.DateEnd.HasValue
            ? DateOnly.FromDateTime(filter.DateEnd.Value)
            : accidents.Max(s => DateOnly.FromDateTime(s.Date));
        var firstPeriod = GetPeriodStart(firstDate, groupBy);
        var lastPeriod = GetPeriodStart(lastDate, groupBy);
        var counts = accidents
            .GroupBy(s => GetPeriodStart(DateOnly.FromDateTime(s.Date), groupBy))
            .ToDictionary(g => g.Key, g => g.Count());
        var periods = new List<SinistroPeriodStatistic>();
        var period = firstPeriod;

        while (period <= lastPeriod)
        {
            periods.Add(new SinistroPeriodStatistic
            {
                PeriodStart = period,
                Count = counts.GetValueOrDefault(period)
            });

            if (period == lastPeriod)
                break;

            period = GetNextPeriod(period, groupBy);
        }

        return periods;
    }

    private static DateOnly GetPeriodStart(DateOnly date, string groupBy) => groupBy switch
    {
        "week" => date.AddDays(-(((int)date.DayOfWeek + 6) % 7)),
        "month" => new DateOnly(date.Year, date.Month, 1),
        _ => date
    };

    private static DateOnly GetNextPeriod(DateOnly period, string groupBy) => groupBy switch
    {
        "week" => period.AddDays(7),
        "month" => period.AddMonths(1),
        _ => period.AddDays(1)
    };

    public void Dispose()
    {
        _sinistroRepository.Dispose();
        _personService.Dispose();
        _vehicleService.Dispose();
    }    
}
