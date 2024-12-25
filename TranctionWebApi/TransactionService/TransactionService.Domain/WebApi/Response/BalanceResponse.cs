namespace TransactionService.Domain.WebApi.Response
{
    public class BalanceResponse
    {
        public decimal ClientBalance { get; set; }

        public DateTime BalanceDateTime { get; set; }
    }
}