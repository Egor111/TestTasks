using TransactionService.Contract.Models;
using TransactionService.Domain.WebApi.Request;
using TransactionService.Domain.WebApi.Response;

namespace TransactionService.Domain.BusinessLayer.Interfaces
{
    public interface IDebitTransactionService
    {
        /// <summary>
        /// Списание средст со счета клиента
        /// </summary>
        Task<Result<TransactionResponse>> DebitAsync(TransactionRequest request, CancellationToken cancellationToken = default);
    }
}
