using Api_InfoTransito.DTOs.Events;
using Api_InfoTransito.DTOs.Location;
using Api_InfoTransito.Extensions;
using AutoMapper;
using Business_InfoTransito.Interfaces;
using Business_InfoTransito.Interfaces.IRepositories.Events;
using Business_InfoTransito.Interfaces.IRepositories.Location;
using Business_InfoTransito.Interfaces.IRepositories.People;
using Business_InfoTransito.Interfaces.IServices.Events;
using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_InfoTransito.Controllers.Events;

[Authorize]
[ApiController]
[Route("sinistros")]
public class SinistroController : MainController
{
    private readonly ISinistroRepository _sinistroRepository;
    private readonly ISinistroAddressRepository _sinistroAddressRepository;
    private readonly ISinistroService _sinistroService;
    private readonly IPersonRepository _personRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;

    public SinistroController(ISinistroRepository sinistroRepository,
        ISinistroAddressRepository sinistroAddressRepository,
        ISinistroService sinistroService,
        IPersonRepository personRepository,
        IVehicleRepository vehicleRepository,
        IMapper mapper, INotifier notifier) : base(notifier)
    {
        _mapper = mapper;
        _sinistroRepository = sinistroRepository;
        _sinistroAddressRepository = sinistroAddressRepository;
        _sinistroService = sinistroService;
        _personRepository = personRepository;
        _vehicleRepository = vehicleRepository;
    }

    [ClaimsAuthorize("Sinistros", "VI")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        return Ok(_mapper.Map<List<SinistroDto>>(await _sinistroRepository.GetAll()));
    }


    [ClaimsAuthorize("Sinistros", "VI")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sinistroDto = await FindSinistro(id);
  
        if (sinistroDto == null) return NotFound();

        return Ok(sinistroDto);
    }

    [ClaimsAuthorize("Sinistros", "AD")]
    [HttpPost]
    public async Task<ActionResult<SinistroDto>> Add( SinistroDto sinistroDto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        await _sinistroService.Add(_mapper.Map<Sinistro>(sinistroDto));

        return CustomResponse(sinistroDto);
    }

    [ClaimsAuthorize("Sinistros", "ED")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SinistroDto>> Update(Guid id,[FromBody] SinistroDto sinistroDto)
    {
        if (id != sinistroDto.Id)
        {
            NotifyError("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(sinistroDto);
        }
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        await _sinistroService.Update(_mapper.Map<Sinistro>(sinistroDto));

        return CustomResponse(sinistroDto);
    }


    [ClaimsAuthorize("Sinistros", "ED")]
    [HttpPut("editarEnderecoSinistro/{id:guid}")]
    public async Task<ActionResult<SinistroDto>> SinistroAddressUpdate(Guid id, SinistroDto sinistroDto)
    {
        if (id != sinistroDto.Id)
        {
            NotifyError("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(sinistroDto);
        }
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        await _sinistroService.UpdateSinistroAddress(_mapper.Map<Sinistro>(sinistroDto));

        return CustomResponse(sinistroDto);
    }

    //aux metods
    private async Task<SinistroDto> FindSinistro(Guid id)
    {
        var sinistro = _mapper.Map<SinistroDto>(await _sinistroRepository.GetSinistroAllData(id));

        return sinistro;
    }
}
