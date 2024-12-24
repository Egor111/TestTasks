using TransactionService.Contract.Entities;
using TransactionService.Contract.Interfaces;
using TransactionService.Domain.DataLayer.Enums;

namespace TransactionService.Domain.DataLayer.Entities
{
    public class Transaction : BaseEntity, ITransaction
    {
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public bool IsReverted { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
