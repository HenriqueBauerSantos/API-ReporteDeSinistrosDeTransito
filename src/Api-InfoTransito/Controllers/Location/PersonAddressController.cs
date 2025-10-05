using AutoMapper;
using Business_InfoTransito.Interfaces;
using Business_InfoTransito.Interfaces.IRepositories.Location;
using Microsoft.AspNetCore.Mvc;

namespace Api_InfoTransito.Controllers.Location;

[ApiController]
[Route("api/[controller]")]
public class PersonAddressController : MainController
{
    private readonly IPersonAddressRepository _personAddressRepository;
    private readonly IMapper _mapper;


    public PersonAddressController(IPersonAddressRepository personAddressRepository,
        IMapper mapper,
        INotifier notifier) : base(notifier)
    {
        _mapper = mapper;
        _personAddressRepository = personAddressRepository;
    }


}
