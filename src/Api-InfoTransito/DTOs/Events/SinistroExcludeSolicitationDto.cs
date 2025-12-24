using Business_InfoTransito.Enums;

namespace Api_InfoTransito.DTOs.Events;

public class SinistroExcludeSolicitationDto
{
    public Guid Id { get; set; }
    public Guid SinistroId { get; set; }
    public string? Motive { get; set; }
    public DateTime RequestDate { get; set; }
    public ExclusionStatus Status { get; set; }
    public Guid AnalyzedByUserId { get; set; }
    public DateTime AnalyzedDate { get; set; }
}
