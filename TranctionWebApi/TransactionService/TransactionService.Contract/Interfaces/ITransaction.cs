namespace TransactionService.Contract.Interfaces
{
    public interface ITransaction : IEntity
    {
        Guid ClientId { get; }
        DateTime DateTime { get; }
        decimal Amount { get; }
    }
}