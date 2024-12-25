using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransactionService.Contract;
using TransactionService.Contract.Enums;
using TransactionService.Contract.Models;
using TransactionService.Domain.BusinessLayer.Interfaces;
using TransactionService.Domain.DataLayer.Entities;
using TransactionService.Domain.WebApi.Response;

namespace TransactionService.Domain.BusinessLayer.Services
{
    public class RevertTransactionService : IRevertTransactionService
    {
        private readonly IDataStore _dataStore;
        private readonly ILogger<RevertTransactionService> _logger;

        public RevertTransactionService(IDataStore dataStore, ILogger<RevertTransactionService> logger)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<TransactionResponse>> RevertAsync(Guid transactionId, CancellationToken cancellationToken = default)
        {
            try
            {
                var transactionModel = await _dataStore.GetAll<TransactionEntity>()
                    .Where(x => x.Id == transactionId)
                    .Select(x => new { Transaction = x, ClientBalance = x.Client.Balance })
                    .SingleOrDefaultAsync(cancellationToken);

                if (transactionModel == null)
                {
                    return Result.CreateFailure<TransactionResponse>(EErrorType.BadRequest, $"По идентификатору {transactionId} не найдена транзакция для отмены");
                }

                transactionModel.Transaction.IsReverted = true;

                await _dataStore.UpdateAsync(transactionModel.Transaction, cancellationToken);

                return Result.CreateSuccess(new TransactionResponse { ClientBalance = transactionModel.ClientBalance, InsertDateTime = transactionModel.Transaction.DateTime });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.CreateFailure<TransactionResponse>(EErrorType.BadRequest, ex.Message);
            }
        }        
    }
}