using Business_InfoTransito.Models.Events;
using FluentValidation;

namespace Business_InfoTransito.Models.Validations.Events;

public class VehicleValidation : AbstractValidator<Vehicle>
{
    public VehicleValidation()
    {
        RuleFor(x => x.CarLisencePlate)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(7).WithMessage("The {PropertyName} field must have {MinLength} charecters");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(2, 50).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} charecters");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(2, 50).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} charecters");

        RuleFor(x => x.Color)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(2, 50).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} charecters");

        RuleFor(x => x.ManufacturingYear)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        //RuleFor(x => x.TypeVehicle)
        //    .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");
    }
}
