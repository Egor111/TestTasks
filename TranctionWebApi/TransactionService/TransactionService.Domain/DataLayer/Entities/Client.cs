using TransactionService.Contract.Entities;

namespace TransactionService.Domain.DataLayer.Entities
{
    public class Client : BaseEntity
    {
        /// <summary>
        /// Баланс
        /// </summary>
        public decimal Balance { get; set; }
    }
}