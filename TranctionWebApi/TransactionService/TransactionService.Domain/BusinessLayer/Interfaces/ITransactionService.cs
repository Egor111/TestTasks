using TransactionService.Contract.Models;
using TransactionService.Domain.WebApi.Request;
using TransactionService.Domain.WebApi.Response;

namespace TransactionService.Domain.BusinessLayer.Interfaces
{
    public interface ITransactionService
    {
        /// <summary>
        /// Зачисление средст на счет клиента
        /// </summary>
        Task<Result<BalanceResponse>> CreditTransactionAsync(TransactionRequest request);

        /// <summary>
        /// Списание средст со счета клиента
        /// </summary>
        Task<Result> DebitTransactionAsync(TransactionRequest request);

        /// <summary>
        /// Отмена транзакции
        /// </summary>
        Task<Result> RevertTransactionAsync(Guid transactionId);

        /// <summary>
        /// Получение баланса клиента
        /// </summary>
        Task<Result<decimal>> GetClientBalanceAsync(Guid clientId);
    }
}