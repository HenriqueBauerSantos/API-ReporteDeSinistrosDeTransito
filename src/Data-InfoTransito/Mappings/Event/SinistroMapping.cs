using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_InfoTransito.Mappings.Event;

public class SinistroMapping : IEntityTypeConfiguration<Sinistro>
{
    public void Configure(EntityTypeBuilder<Sinistro> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.InjuredPeople)
            .IsRequired();

        builder.Property(x => x.SinistroType)
            .IsRequired();

        builder.Property(x => x.RoadPavementType)
            .IsRequired();

        builder.Property(x => x.RoadType)
            .IsRequired();

        builder.Property(x => x.GroundType)
            .IsRequired();

        builder.Property(x => x.Weather)
            .IsRequired();

        builder.Property(x => x.SinistroDescription)
            .IsRequired();

        //SinistroAddress SinistroAddress 
        builder.HasOne(x => x.SinistroAddress)
            .WithOne(a => a.SinistroRegister)
            .HasForeignKey<SinistroAddress>(a => a.SinistroId)
            .OnDelete(DeleteBehavior.Cascade);

        //List PeopleEnvolved
        builder.HasMany(x => x.PeopleEnvolved)
            .WithOne(p => p.Sinistro)
            .HasForeignKey(p => p.SinistroID)
            .OnDelete(DeleteBehavior.Cascade);

        //List VehiclesEnvolved
        builder.HasMany(x => x.VehiclesEnvolved)
            .WithOne(v => v.Sinistro)
            .HasForeignKey(v => v.SinistroId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Sinistros");
    }
}
