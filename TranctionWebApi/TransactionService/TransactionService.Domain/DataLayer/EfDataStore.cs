using TransactionService.Contract.Interfaces;
using TransactionService.Contract;

namespace TransactionService.Domain.DataLayer
{
    // <summary>
    /// Реализация IdataStore для EntityFramework
    /// </summary>
    public class EfDataStore : IDataStore
    {
        private readonly ClientServerDbContext _context;

        public EfDataStore(
            ClientServerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc /> 
        public virtual IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : class, IEntity
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            return query;
        }     

        /// <inheritdoc /> 
        public virtual async Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc /> 
        public virtual async Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc /> 
        public virtual async Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }      
    }
}