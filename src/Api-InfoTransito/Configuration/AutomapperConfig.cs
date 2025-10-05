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

        CreateMap<PersonAddress, PersonAddressDto>()
            .IncludeBase<Address, AddressDto>()
            .ReverseMap();

        CreateMap<SinistroAddress, SinistroAddressDto>()
            .IncludeBase<Address, AddressDto>()
            .ReverseMap();
    }
}
