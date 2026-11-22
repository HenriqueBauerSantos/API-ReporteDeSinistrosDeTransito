namespace Business_InfoTransito.Models.Events;

public class SinistroStatistics
{
    public int TotalAccidents { get; set; }
    public int AccidentsWithInjuries { get; set; }
    public int AccidentsWithoutInjuries { get; set; }
    public double InjuryRate { get; set; }
    public List<SinistroPeriodStatistic> ByPeriod { get; set; } = [];
    public List<SinistroValueStatistic> ByAccidentType { get; set; } = [];
    public List<SinistroValueStatistic> ByWeather { get; set; } = [];
    public List<SinistroValueStatistic> ByRoadType { get; set; } = [];
    public List<SinistroValueStatistic> ByRoadPavementType { get; set; } = [];
    public List<SinistroValueStatistic> ByGroundType { get; set; } = [];
    public List<SinistroVehicleStatistic> ByVehicleType { get; set; } = [];
    public List<SinistroLocationStatistic> ByLocation { get; set; } = [];
}

public class SinistroPeriodStatistic
{
    public DateOnly PeriodStart { get; set; }
    public int Count { get; set; }
}

public class SinistroValueStatistic
{
    public int Value { get; set; }
    public int Count { get; set; }
}

public class SinistroVehicleStatistic
{
    public int Value { get; set; }
    public int AccidentCount { get; set; }
    public int VehicleCount { get; set; }
}

public class SinistroLocationStatistic
{
    public string? City { get; set; }
    public string? District { get; set; }
    public int Count { get; set; }
}
