using Business_InfoTransito.Enums;

namespace Business_InfoTransito.Models.Events;

public class SinistroExcludeSolicitation : Entity
{
    public Guid? SinistroId { get; set; }
    public string? Motive { get; set; }
    public DateTime RequestDate { get; set; }
    public ExclusionStatus Status { get; set; }
    public Guid AnalyzedByUserId { get; set; }
    public DateTime AnalyzedDate { get; set; }

    //EF
    public Sinistro? Sinistro { get; set; }
}
