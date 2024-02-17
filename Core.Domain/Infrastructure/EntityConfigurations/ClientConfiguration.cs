using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Infrastructure.EntityConfigurations
{
    public class ClientConfiguration:IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients", nameof(Schemas.card));
            builder.HasKey(x => x.ClientId);
            builder.Property(x => x.ClientId).ValueGeneratedNever();

            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.ContactEmail).HasMaxLength(100);

        }
    }
}
