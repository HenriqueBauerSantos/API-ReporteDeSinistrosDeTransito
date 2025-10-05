using Business_InfoTransito.Models.Location;
using Business_InfoTransito.Models.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_InfoTransito.Mappings.People;

public class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.Gender)
            .IsRequired();

        builder.Property(x => x.CPF)
            .IsRequired()
            .HasColumnType("varchar(11)");

        builder.Property(x => x.RG)
            .IsRequired()
            .HasColumnType("varchar(9)");

        builder.Property(x => x.CNH)
            .IsRequired()
            .HasColumnType("varchar(10)");

        builder.Property(x => x.Phone)
            .IsRequired()
            .HasColumnType("varchar(20)");

        //Relacionamento com Address
        builder.HasOne(x => x.Address)
            .WithOne(a => a.person)
            .HasForeignKey<PersonAddress>(a => a.PersonId);

        builder.ToTable("Persons");
    }
}
