namespace Api_InfoTransito.DTOs.Events;

public class SinistroStatisticsDto
{
    public int TotalAccidents { get; set; }
    public int AccidentsWithInjuries { get; set; }
    public int AccidentsWithoutInjuries { get; set; }
    public double InjuryRate { get; set; }
    public List<SinistroPeriodStatisticDto> ByPeriod { get; set; } = [];
    public List<SinistroValueStatisticDto> ByAccidentType { get; set; } = [];
    public List<SinistroValueStatisticDto> ByWeather { get; set; } = [];
    public List<SinistroValueStatisticDto> ByRoadType { get; set; } = [];
    public List<SinistroValueStatisticDto> ByRoadPavementType { get; set; } = [];
    public List<SinistroValueStatisticDto> ByGroundType { get; set; } = [];
    public List<SinistroVehicleStatisticDto> ByVehicleType { get; set; } = [];
    public List<SinistroLocationStatisticDto> ByLocation { get; set; } = [];
}

public class SinistroPeriodStatisticDto
{
    public DateOnly PeriodStart { get; set; }
    public int Count { get; set; }
}

public class SinistroValueStatisticDto
{
    public int Value { get; set; }
    public int Count { get; set; }
}

public class SinistroVehicleStatisticDto
{
    public int Value { get; set; }
    public int AccidentCount { get; set; }
    public int VehicleCount { get; set; }
}

public class SinistroLocationStatisticDto
{
    public string? City { get; set; }
    public string? District { get; set; }
    public int Count { get; set; }
}
