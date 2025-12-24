using Business_InfoTransito.Models.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_InfoTransito.Mappings.Event;

public class SinistroExcludeSolicitationMapping : IEntityTypeConfiguration<SinistroExcludeSolicitation>
{
    public void Configure(EntityTypeBuilder<SinistroExcludeSolicitation> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.SinistroId)
            .IsRequired(false);

        builder.Property(x => x.Motive)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.RequestDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.AnalyzedByUserId)
            .IsRequired();

        builder.Property(x => x.AnalyzedDate)
            .IsRequired();

        builder
            .HasOne(x => x.Sinistro)
            .WithMany() // Apenas se você não tem coleção no model do Sinistro
            .HasForeignKey(x => x.SinistroId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable("SinistroExcludeSolicitation");
    }
}
