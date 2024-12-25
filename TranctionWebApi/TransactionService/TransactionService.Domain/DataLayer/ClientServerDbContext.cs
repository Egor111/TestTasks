using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.DataLayer.Configarations;
using TransactionService.Domain.DataLayer.Entities;

namespace TransactionService.Domain.DataLayer
{
    public class ClientServerDbContext : DbContext
    {   
        public virtual DbSet<ClientEntity> Client { get; set; }

        public virtual DbSet<TransactionEntity> Transaction { get; set; }

        public ClientServerDbContext(DbContextOptions<ClientServerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("transaction_service");

            modelBuilder.ApplyConfiguration(new ClientEntityConfigarations());
            modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration());
        }
    }
}