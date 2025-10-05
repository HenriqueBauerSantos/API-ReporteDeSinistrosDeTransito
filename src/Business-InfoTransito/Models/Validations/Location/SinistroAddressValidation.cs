using Business_InfoTransito.Models.Location;
using FluentValidation;

namespace Business_InfoTransito.Models.Validations.Location;

public class SinistroAddressValidation : AbstractValidator<SinistroAddress>
{
    public SinistroAddressValidation()
    {
        Include(new AddressValidation());

        RuleFor(x => x.Latitude)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.Longitude)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");
    }
}
