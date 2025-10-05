using Business_InfoTransito.Models.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_InfoTransito.Mappings.Location;

public class SinsistroAddressMapping : IEntityTypeConfiguration<SinistroAddress>
{
    public void Configure(EntityTypeBuilder<SinistroAddress> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SinistroId)
            .IsRequired();

        builder.Property(x => x.Road)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(x => x.Number)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(x => x.Complement)
            .HasColumnType("varchar(250)");

        builder.Property(x => x.Cep)
            .IsRequired()
            .HasColumnType("varchar(8)");

        builder.Property(x => x.District)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(x => x.City)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(x => x.State)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(x => x.Latitude)
            .IsRequired();

        builder.Property(x => x.Longitude)
            .IsRequired();

        builder.ToTable("SinistrosAddresses");
    }
}
