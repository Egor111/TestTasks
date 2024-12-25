using TransactionService.Contract.Entities;

namespace TransactionService.Domain.DataLayer.Entities
{
    public class ClientEntity : BaseEntity
    {
        /// <summary>
        /// Баланс
        /// </summary>
        public decimal Balance { get; set; }
    }
}