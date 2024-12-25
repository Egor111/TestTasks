using TransactionService.Contract.Models;
using TransactionService.Domain.WebApi.Response;

namespace TransactionService.Domain.BusinessLayer.Interfaces
{
    public interface IRevertTransactionService
    {
        /// <summary>
        /// Отмена транзакции
        /// </summary>
        Task<Result<TransactionResponse>> RevertAsync(Guid transactionId, CancellationToken cancellationToken = default);
    }
}