using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionService.Domain.DataLayer.Entities;

namespace TransactionService.Domain.DataLayer.Configarations
{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("Transaction").HasKey(x => x.Id);
            builder.Property(p => p.DateTime).IsRequired(true);
            builder.Property(p => p.Amount).IsRequired(true);
            builder.Property(p => p.IsReverted).IsRequired(true);
            builder.Property(p => p.TransactionType).IsRequired(true);

            builder.HasOne(x => x.Client)
                .WithOne()             
                .HasForeignKey<ClientEntity>(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}