namespace TransactionService.Contract.Interfaces
{
    // <summary>
    /// Минимальная сущность
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        Guid Id { get; set; }
    }
}