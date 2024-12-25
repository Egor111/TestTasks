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
    public class ClientService : IClientService
    {
        private readonly IDataStore _dataStore;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IDataStore dataStore, ILogger<ClientService> logger)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<BalanceResponse>> GetBalanceAsync(Guid clientId)
        {
            try
            {
                var balance = await _dataStore.GetAll<ClientEntity>()
                .Where(x => x.Id == clientId)
                .Select(x => new BalanceResponse 
                {
                    ClientBalance = x.Balance,
                    BalanceDateTime = DateTime.UtcNow
                })
                .SingleOrDefaultAsync();

                if (balance == default)
                {
                    var errorMessage = "Клиент не найден";

                    _logger.LogError(errorMessage);
                    return Result.CreateFailure<BalanceResponse>(EErrorType.BadRequest, errorMessage);
                }

                return Result.CreateSuccess(balance);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.CreateFailure<BalanceResponse>(EErrorType.BadRequest, ex.Message);
            }
        }
    }
}