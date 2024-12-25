using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Domain.DataLayer;

namespace TransactionService.Domain.BusinessLayer
{
    /// <summary>
    /// Конфигурация для PostrgeSql
    /// </summary>
    public static class ConfigurationPostgreExtensions
    {
        public static void AddPostgreSqlReplicaDb(this IServiceCollection service, string connectionString)
        {
            service.AddDbContext<ClientServerDbContext>(options => options.UseNpgsql(connectionString));
        }

        public static void AddPostgreSqlMifrationReplicaDb(this IServiceCollection service, string connectionString)
        {
            var provider = service
                .AddFluentMigratorCore()
                .ConfigureRunner(config => config.AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(ClientServerDbContext).Assembly).For.Migrations())
                .AddLogging(x => x.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            using (var scope = provider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }
    }
}
