using Business_InfoTransito.Models.Events;
using FluentValidation;

namespace Business_InfoTransito.Models.Validations.Events;

public class SinistroValidation : AbstractValidator<Sinistro>
{
    public SinistroValidation()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.InjuredPeople)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.SinistroType)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.RoadPavementType)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.RoadType)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.GroundType)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.Weather)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.SinistroDescription)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");
    }
}
