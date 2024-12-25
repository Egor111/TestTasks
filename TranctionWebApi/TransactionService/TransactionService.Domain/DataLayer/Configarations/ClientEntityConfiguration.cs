using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.DataLayer.Entities;

namespace TransactionService.Domain.DataLayer.Configarations
{
    public class ClientEntityConfigarations : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder.ToTable("Client").HasKey(x => x.Id);
            builder.Property(p => p.Balance).IsRequired(true);
        }
    }
}