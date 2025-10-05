using Business_InfoTransito.Models.People;
using System;

namespace Business_InfoTransito.Models.Location;

public class PersonAddress : Address
{
    public Guid PersonId { get; set; }

    /*EF Relation*/
    public Person person { get; set; }
}
