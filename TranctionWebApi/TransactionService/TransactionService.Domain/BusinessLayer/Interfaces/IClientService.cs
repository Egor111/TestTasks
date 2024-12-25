using TransactionService.Contract.Models;
using TransactionService.Domain.WebApi.Response;

namespace TransactionService.Domain.BusinessLayer.Interfaces
{
    public interface IClientService
    {
        /// <summary>
        /// Получение баланса клиента
        /// </summary>
        Task<Result<BalanceResponse>> GetBalanceAsync(Guid clientId);
    }
}