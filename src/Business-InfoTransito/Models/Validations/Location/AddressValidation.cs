using Business_InfoTransito.Models.Location;
using FluentValidation;

namespace Business_InfoTransito.Models.Validations.Location;

public class AddressValidation : AbstractValidator<Address>
{
    public AddressValidation()
    {
        RuleFor(c => c.Road)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(2, 200).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} charecters");

        RuleFor(c => c.Number)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(1, 50).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} charecters");

        RuleFor(c => c.Cep)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(8, 8).WithMessage("The {PropertyName} field must have {MinLength} charecters");

        RuleFor(c => c.District)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} charecters");

        RuleFor(c => c.City)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} charecters");

        RuleFor(c => c.State)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(2, 2).WithMessage("The {PropertyName} field must have {MinLength} charecters");
    }
}
