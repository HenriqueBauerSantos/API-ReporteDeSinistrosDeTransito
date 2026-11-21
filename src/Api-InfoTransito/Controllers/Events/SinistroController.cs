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
using System.Security.Claims;

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
        return CustomResponse(_mapper.Map<List<SinistroDto>>(await _sinistroRepository.GetAll()));
    }

    [ClaimsAuthorize("Sinistros", "VI")]
    [HttpGet("mapa")]
    public async Task<IActionResult> GetMapMarkers([FromQuery] SinistroMapFilterDto filterDto)
    {
        var filter = _mapper.Map<SinistroMapFilter>(filterDto);
        var markers = await _sinistroRepository.GetByMapFilter(filter);

        return CustomResponse(_mapper.Map<List<SinistroMapMarkerDto>>(markers));
    }

    [ClaimsAuthorize("Sinistros", "VI")]
    [HttpGet("estatisticas")]
    public async Task<IActionResult> GetStatistics([FromQuery] SinistroStatisticsFilterDto filterDto)
    {
        var filter = _mapper.Map<SinistroStatisticsFilter>(filterDto);
        var statistics = await _sinistroService.GetStatistics(filter);
        var message = statistics.TotalAccidents == 0 ? "Nenhum registro encontrado." : null;

        return CustomResponse(_mapper.Map<SinistroStatisticsDto>(statistics), message);
    }

    [ClaimsAuthorize("Sinistros", "AD")]
    [HttpGet("meus-registros")]
    public async Task<IActionResult> GetAllByUser()
    {
        var userId = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value
            ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(userId == null)
            return CustomResponseNotFound("O usuário não foi encontrado.");

        return CustomResponse(_mapper.Map<List<SinistroDto>>(await _sinistroRepository.GetByUserId(userId)));
    }


    [ClaimsAuthorize("Sinistros", "VI")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sinistroDto = await FindSinistro(id);

        if (sinistroDto == null) return CustomResponseNotFound();

        return CustomResponse(sinistroDto);
    }

    [ClaimsAuthorize("Sinistros", "AD")]
    [HttpPost]
    public async Task<ActionResult<SinistroDto>> Add( SinistroDto sinistroDto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var userId = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value
            ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var sinistroToAdd = _mapper.Map<Sinistro>(sinistroDto);
        sinistroToAdd.UserId = userId;

        await _sinistroService.Add(sinistroToAdd);

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

    [ClaimsAuthorize("Sinistros", "EX")]
    [HttpDelete("solicitacaoDeletarSinistro/{id:guid}")]
    public async Task<ActionResult<SinistroDto>> RequestDelete(Guid id)
    {
        var sinistroDto = await FindSinistro(id);

        if (sinistroDto == null) return CustomResponseNotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _sinistroService.DeleteSolicitation(id, Guid.Parse(userId));

        return CustomResponse(sinistroDto);
    }

    [ClaimsAuthorize("Sinistros", "CANEX")]
    [HttpGet("ListasolicitacaoDeletarSinistro")]
    public async Task<ActionResult<SinistroDto>> RequestList()
    {
        return CustomResponse(_mapper.Map<List<SinistroDto>>(await _sinistroRepository.GetSinistrosComSolicitacaoCompleto()));
    }

    [ClaimsAuthorize("Sinistros", "CANEX")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<SinistroDto>> Delete(Guid id)
    {
        var sinistroDto = await FindSinistro(id);

        if (sinistroDto == null) return CustomResponseNotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _sinistroService.Delete(id, Guid.Parse(userId));

        return CustomResponse(sinistroDto);
    }

    //aux metods
    private async Task<SinistroDto> FindSinistro(Guid id)
    {
        var sinistro = _mapper.Map<SinistroDto>(await _sinistroRepository.GetSinistroAllData(id));

        return sinistro;
    }
}
