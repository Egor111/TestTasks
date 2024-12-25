using Microsoft.EntityFrameworkCore;
using TransactionService.Domain.BusinessLayer;
using TransactionService.Domain.Ioc;

namespace TransactionService.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection service)
        {
            var configuration = hostContext.Configuration;
            var connectionString = configuration.GetConnectionString("Context");           ;

            // PostgreSql
            service.AddPostgreSqlReplicaDb(connectionString);
            service.AddPostgreSqlMifrationReplicaDb(connectionString);

            service.AddService();
        }
    }
}
