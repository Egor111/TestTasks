using Microsoft.Extensions.DependencyInjection;
using TransactionService.Contract;
using TransactionService.Domain.BusinessLayer.Interfaces;
using TransactionService.Domain.BusinessLayer.Services;
using TransactionService.Domain.DataLayer;

namespace TransactionService.Domain.Ioc
{
    public static class RegisterService
    {
        /// <summary>
        /// Регистрируем сервисы необходимые для работы
        /// </summary>
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IDataStore, EfDataStore>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<ICreditTransactionService, CreditTransactionService>();
            services.AddTransient<IDebitTransactionService, DebitTransactionService>();
            services.AddTransient<IRevertTransactionService, RevertTransactionService>();
        }
    }
}