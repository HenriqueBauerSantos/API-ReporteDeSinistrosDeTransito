using Business_InfoTransito.Models;
using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.Location;

namespace Business_InfoTransito.Interfaces.IServices.Events;

public interface ISinistroService : IDisposable
{
    Task Add(Sinistro sinistro);
    Task Update(Sinistro sinistro);
    Task Delete(Guid id, Guid userId);
    Task UpdateSinistroAddress(Sinistro sinistro);
    Task DeleteSolicitation(Guid id, Guid userId);
}
