using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransactionService.Contract;
using TransactionService.Contract.Enums;
using TransactionService.Contract.Models;
using TransactionService.Domain.BusinessLayer.Interfaces;
using TransactionService.Domain.DataLayer.Entities;
using TransactionService.Domain.WebApi.Request;
using TransactionService.Domain.WebApi.Response;

namespace TransactionService.Domain.BusinessLayer.Services
{
    public class DebitTransactionService : IDebitTransactionService
    {
        private readonly IDataStore _dataStore;
        private readonly ILogger<DebitTransactionService> _logger;

        public DebitTransactionService(IDataStore dataStore, ILogger<DebitTransactionService> logger)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<TransactionResponse>> DebitAsync(TransactionRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var validateResult = ValidateRequest(request);

                if (!validateResult.IsSuccess)
                {
                    return Result.CreateFailure<TransactionResponse>(EErrorType.BadRequest, validateResult.Error.Message!);
                }

                var transactionResponse = await _dataStore.GetAll<TransactionEntity>()
                    .Where(x => x.Id == request.Id)
                    .Select(x => new TransactionResponse
                    {
                        ClientBalance = x.Client.Balance,
                        InsertDateTime = x.DateTime,
                    })
                    .SingleOrDefaultAsync(cancellationToken);

                if (transactionResponse != null)
                {
                    return Result.CreateSuccess(transactionResponse);
                }

                var client = await _dataStore.GetAll<ClientEntity>()
                    .Where(x => x.Id == request.ClientId)
                    .SingleOrDefaultAsync(cancellationToken);

                if (client == null)
                {
                    var errorMessage = $"Клиент c идентификатором {request.ClientId} не найден";

                    _logger.LogError(errorMessage);
                    return Result.CreateFailure<TransactionResponse>(EErrorType.BadRequest, errorMessage);
                }

                if (client.Balance < request.Amount) 
                {
                    var errorMessage = $"У клиента {request.ClientId} недостаточно средств";

                    _logger.LogError(errorMessage);
                    return Result.CreateFailure<TransactionResponse>(EErrorType.BadRequest, errorMessage);
                }

                client!.Balance -= request.Amount;
                await _dataStore.UpdateAsync(client, cancellationToken);

                var creditTransaction = new TransactionEntity
                {
                    Id = Guid.NewGuid(),
                    ClientId = request.ClientId,
                    Client = client!,
                    DateTime = request.DateTime,
                    Amount = request.Amount,
                    TransactionType = DataLayer.Enums.TransactionType.DebitTransaction
                };

                await _dataStore.SaveAsync(creditTransaction, cancellationToken);

                return Result.CreateSuccess(new TransactionResponse { ClientBalance = client.Balance, InsertDateTime = creditTransaction.DateTime });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.CreateFailure<TransactionResponse>(EErrorType.BadRequest, ex.Message);
            }
        }

        private Result ValidateRequest(TransactionRequest request)
        {
            if (request.Amount <= 0)
            {
                return Result.CreateFailure(EErrorType.BadRequest, "Сумма должна быть положительной");
            }

            if (request.DateTime.Date > DateTime.UtcNow.Date)
            {
                return Result.CreateFailure(EErrorType.BadRequest, "Дата не может быть в будущем");
            }

            return Result.CreateFailure();
        }
    }
}