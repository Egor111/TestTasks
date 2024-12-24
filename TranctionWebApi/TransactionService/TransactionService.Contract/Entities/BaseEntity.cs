using System.Security.Principal;
using TransactionService.Contract.Interfaces;

namespace TransactionService.Contract.Entities
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}