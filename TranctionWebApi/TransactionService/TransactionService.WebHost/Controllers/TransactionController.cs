using Microsoft.AspNetCore.Mvc;
using TransactionService.Contract.Models;
using TransactionService.Domain.BusinessLayer.Interfaces;
using TransactionService.Domain.WebApi.Request;
using TransactionService.Domain.WebApi.Response;

namespace TransactionService.WebHost.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionController : ControllerBase
    {
        private readonly IClientService _сlientService;
        private readonly ICreditTransactionService _creditTransactionService;
        private readonly IDebitTransactionService _debitTransactionService;
        private readonly IRevertTransactionService _transactionService;

        public TransactionController(
            IClientService сlientService,
            ICreditTransactionService creditTransactionService,
            IDebitTransactionService debitTransactionService,
            IRevertTransactionService transactionService)
        {       
            _сlientService = сlientService ?? throw new ArgumentNullException(nameof(сlientService));
            _creditTransactionService = creditTransactionService ?? throw new ArgumentNullException(nameof(creditTransactionService));
            _debitTransactionService = debitTransactionService ?? throw new ArgumentNullException(nameof(debitTransactionService));
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [HttpPost("credit")]
        public Task<Result<TransactionResponse>> Credit([FromBody] TransactionRequest request)
        {
            return _creditTransactionService.CreditAsync(request);
        }

        [HttpPost("debit")]
        public Task<Result<TransactionResponse>> Debit([FromBody] TransactionRequest request)
        {
            return _debitTransactionService.DebitAsync(request);
        }

        [HttpPost("revert")]
        public Task<Result<TransactionResponse>> Revert([FromQuery] Guid transactionId)
        {
            return _transactionService.RevertAsync(transactionId);
        }

        [HttpGet("balance")]
        public Task<Result<BalanceResponse>> GetBalance([FromQuery] Guid clientId)
        {        
            return _сlientService.GetBalanceAsync(clientId);
        }
    }
}
