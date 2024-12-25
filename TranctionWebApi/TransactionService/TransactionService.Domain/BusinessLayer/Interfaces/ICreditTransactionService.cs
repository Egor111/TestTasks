using TransactionService.Contract.Models;
using TransactionService.Domain.WebApi.Request;
using TransactionService.Domain.WebApi.Response;

namespace TransactionService.Domain.BusinessLayer.Interfaces
{
    public interface ICreditTransactionService
    {
        /// <summary>
        /// Зачисление средст на счет клиента
        /// </summary>
        Task<Result<TransactionResponse>> CreditAsync(TransactionRequest request, CancellationToken cancellationToken = default);
    }
}