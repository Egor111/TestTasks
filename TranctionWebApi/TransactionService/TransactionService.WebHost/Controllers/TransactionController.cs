using Microsoft.AspNetCore.Mvc;
using TransactionService.Domain.BusinessLayer.Interfaces;
using TransactionService.Domain.WebApi.Request;

namespace TransactionService.WebHost.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        [HttpPost("credit")]
        public async Task<IActionResult> Credit([FromBody] TransactionRequest request)
        {
            var result = await _transactionService.CreditTransactionAsync(request);

            if (!result.IsSuccess)
                return BadRequest(new { error = result.ErrorMessage });

            return Ok(result.Value);
        }

        [HttpPost("debit")]
        public async Task<IActionResult> Debit([FromBody] TransactionRequest request)
        {
            var result = await _transactionService.DebitTransactionAsync(request);

            if (!result.IsSuccess) 
            {
                return BadRequest(new { error = result.ErrorMessage });
            }

            return Ok();
        }

        [HttpPost("revert")]
        public async Task<IActionResult> Revert([FromQuery] Guid transactionId)
        {
            var result = await _transactionService.RevertTransactionAsync(transactionId);

            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.ErrorMessage });
            }

            return Ok();
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance([FromQuery] Guid clientId)
        {
            var result = await _transactionService.GetClientBalanceAsync(clientId);

            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.ErrorMessage });
            }

            return Ok(result.Value);
        }
    }
}
