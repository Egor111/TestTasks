using Microsoft.Extensions.Logging;
using TransactionService.Contract;
using TransactionService.Contract.Models;
using TransactionService.Domain.BusinessLayer.Interfaces;
using TransactionService.Domain.DataLayer.Entities;
using TransactionService.Domain.WebApi.Request;
using TransactionService.Domain.WebApi.Response;

namespace TransactionService.Domain.BusinessLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IDataStore _dataStore;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(IDataStore dataStore, ILogger<TransactionService> logger)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<BalanceResponse>> CreditTransactionAsync(TransactionRequest request)
        {
            ValidateRequest(request);

            var existingTransaction = await _transactionRepository.GetTransactionAsync(request.Id);
            if (existingTransaction != null)
            {
                return new BalanceResponse { Balance = existingTransaction.Client.Balance };
            }
                
            var client = await _transactionRepository.GetOrCreateClientAsync(request.ClientId);
            client.Balance += request.Amount;

            await _dataStore.SaveAsync(new CreditTransaction
            {
                Id = request.Id,
                ClientId = request.ClientId,
                DateTime = request.DateTime,
                Amount = request.Amount
            });

            return Result.CreateSuccess(new BalanceResponse { Balance = client.Balance });
        }

        public Task<Result> DebitTransactionAsync(TransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<decimal?>> GetClientBalanceAsync(Guid clientId)
        {
            var balance = await _transactionService.GetBalanceAsync(clientId);

            if (balance == null)
            {
                return Result.CreateFailure<decimal>("Client not found.");
            }

            return Result.CreateSuccess(balance);
        }

        public Task<Result> RevertTransactionAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}