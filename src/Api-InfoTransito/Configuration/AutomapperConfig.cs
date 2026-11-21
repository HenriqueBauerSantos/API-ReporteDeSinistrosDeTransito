using Api_InfoTransito.DTOs.Events;
using Api_InfoTransito.DTOs.Location;
using Api_InfoTransito.DTOs.People;
using AutoMapper;
using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.Location;
using Business_InfoTransito.Models.People;

namespace Api_InfoTransito.Configuration;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<Sinistro, SinistroDto>().ReverseMap();
        CreateMap<SinistroMapMarker, SinistroMapMarkerDto>().ReverseMap();
        CreateMap<SinistroStatisticsFilter, SinistroStatisticsFilterDto>().ReverseMap();
        CreateMap<SinistroStatistics, SinistroStatisticsDto>().ReverseMap();
        CreateMap<SinistroPeriodStatistic, SinistroPeriodStatisticDto>().ReverseMap();
        CreateMap<SinistroValueStatistic, SinistroValueStatisticDto>().ReverseMap();
        CreateMap<SinistroVehicleStatistic, SinistroVehicleStatisticDto>().ReverseMap();
        CreateMap<SinistroLocationStatistic, SinistroLocationStatisticDto>().ReverseMap();
        CreateMap<SinistroExcludeSolicitation, SinistroExcludeSolicitationDto>().ReverseMap();

        CreateMap<PersonAddress, PersonAddressDto>()
            .IncludeBase<Address, AddressDto>()
            .ReverseMap();

        CreateMap<SinistroAddress, SinistroAddressDto>()
            .IncludeBase<Address, AddressDto>()
            .ReverseMap();

        CreateMap<SinistroMapFilter, SinistroMapFilterDto>().ReverseMap();
    }
}
