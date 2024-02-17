using Core.Domain.Entities.CardModule.Aggregates;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Infrastructure.EntityConfigurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards", nameof(Schemas.card));
            builder.HasKey(x => x.CardId);
            builder.Property(x => x.CardId).ValueGeneratedNever();

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.Color).HasMaxLength(7);

        }
    }
}
