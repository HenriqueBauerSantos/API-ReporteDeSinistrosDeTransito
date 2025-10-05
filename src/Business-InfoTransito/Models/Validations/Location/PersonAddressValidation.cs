using Business_InfoTransito.Models.Location;
using FluentValidation;

namespace Business_InfoTransito.Models.Validations.Location;

public class PersonAddressValidation : AbstractValidator<PersonAddress>
{
    public PersonAddressValidation()
    {
        Include(new AddressValidation());
    }
}
