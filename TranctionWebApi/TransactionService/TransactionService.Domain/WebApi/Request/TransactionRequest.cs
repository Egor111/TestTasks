namespace TransactionService.Domain.WebApi.Request
{
    public class TransactionRequest
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
    }
}