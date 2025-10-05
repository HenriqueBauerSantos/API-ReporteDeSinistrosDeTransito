using Business_InfoTransito.Models.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_InfoTransito.Mappings.Event;

public class VehicleMapping : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x =>x.CarLisencePlate)
            .IsRequired()
            .HasColumnType("varchar(7)");

        builder.Property(x => x.Brand)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(x => x.Model)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(x => x.Color)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(x => x.TypeVehicle)
            .IsRequired();

        builder.Property(x => x.ManufacturingYear)
            .IsRequired();

        builder.Property(x => x.PersonId)
            .IsRequired();

        builder.ToTable("Vehicles");
    }
}
