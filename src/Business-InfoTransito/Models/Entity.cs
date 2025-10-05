using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_InfoTransito.Models;

[NotMapped]
public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }
}
