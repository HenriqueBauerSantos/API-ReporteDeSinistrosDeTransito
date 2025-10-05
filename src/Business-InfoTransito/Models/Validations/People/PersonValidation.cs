using Business_InfoTransito.Models.People;
using Business_InfoTransito.Models.Validations.Documents;
using FluentValidation;

namespace Business_InfoTransito.Models.Validations.People;

public class PersonValidation : AbstractValidator<Person>
{
    public PersonValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(2, 200).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} charecters");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided");

        RuleFor(x => x.CPF.Length).Equal(CpfValidacao.TamanhoCpf)
            .WithMessage("The Document field must have {ComparisonValue} characters and was provided {PropertyValue}.");
        //RuleFor(x => CpfValidacao.Validar(x.CPF)).Equal(true)
        //    .WithMessage("The document provided is invalid.");

        RuleFor(x => x.RG)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(9).WithMessage("The {PropertyName} field must have {MinLength} charecters");

        RuleFor(x => x.CNH)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(10).WithMessage("The {PropertyName} field must have {MinLength} charecters");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("The {PropertyName} field needs to be provided")
            .Length(2, 20).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} charecters");

    }
}
