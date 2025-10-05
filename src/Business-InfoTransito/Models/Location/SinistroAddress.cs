using Business_InfoTransito.Models.Events;

namespace Business_InfoTransito.Models.Location;

public class SinistroAddress : Address
{
    public Guid SinistroId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    //EF
    public Sinistro SinistroRegister { get; set; }
}
