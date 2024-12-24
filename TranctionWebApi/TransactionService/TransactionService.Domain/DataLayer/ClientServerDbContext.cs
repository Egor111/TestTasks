using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TransactionService.Domain.DataLayer.Entities;

namespace TransactionService.Domain.DataLayer
{
    public class ClientServerDbContext : DbContext
    {
        public virtual DbSet<LinkingUserAndReviewerSchedule> LinkingUserAndReviewerSchedule { get; set; }

        public virtual DbSet<ClientEntity> Client { get; set; }

        public virtual DbSet<Transaction> Transaction { get; set; }

        public ClientServerDbContext(DbContextOptions<ClientServerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("reviewer_demon_service");

            modelBuilder.ApplyConfiguration(new ReviewerScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LinkingUserAndReviewerScheduleConfiguration());
        }
    }
}
