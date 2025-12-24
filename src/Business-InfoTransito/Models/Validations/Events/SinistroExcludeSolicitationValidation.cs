using Business_InfoTransito.Models.Events;
using FluentValidation;

namespace Business_InfoTransito.Models.Validations.Events;

public class SinistroExcludeSolicitationValidation : AbstractValidator<SinistroExcludeSolicitation>
{
    public SinistroExcludeSolicitationValidation()
    {
        RuleFor(x => x.Motive)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");
    }
}
