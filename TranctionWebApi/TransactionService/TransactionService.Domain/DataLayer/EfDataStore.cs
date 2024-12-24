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
        public virtual TEntity Get<TEntity>(long id)
            where TEntity : class, IEntity
        {
            return GetAsync<TEntity>(id).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc /> 
        public virtual async Task<TEntity> GetAsync<TEntity>(long id, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            return await _context.Set<TEntity>().FindAsync(id, cancellationToken);
        }

        /// <inheritdoc /> 
        public virtual void Save<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
        //=> SaveAsync(entity).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <inheritdoc /> 
        public virtual async Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc /> 
        public virtual void Save<TEntity>(List<TEntity> entities) where TEntity : class, IEntity
        {
            foreach (var entity in entities)
            {
                _context.Add(entity);
            }

            _context.SaveChanges();
        }

        /*=> SaveAsync(entities).ConfigureAwait(false).GetAwaiter().GetResult();*/

        /// <inheritdoc /> 
        public virtual async Task SaveAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            foreach (var entity in entities)
            {
                await _context.AddAsync(entity, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

        }

        /// <inheritdoc /> 
        public virtual void Update<TEntity>(TEntity entity) where TEntity : class, IEntity => UpdateAsync(entity).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <inheritdoc /> 
        public virtual void Update<TEntity>(List<TEntity> entities) where TEntity : class, IEntity => UpdateAsync(entities).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <inheritdoc /> 
        public virtual async Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc /> 
        public virtual async Task UpdateAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            foreach (var entity in entities)
            {
                _context.Update(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc /> 
        public virtual void Delete(IEntity entity) => DeleteAsync(entity).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <inheritdoc /> 
        public virtual void Delete(List<IEntity> entities) => DeleteAsync(entities).ConfigureAwait(false).GetAwaiter().GetResult();

        /// <inheritdoc /> 
        public virtual async Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc /> 
        public virtual async Task DeleteAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            foreach (var entity in entities)
            {
                _context.Remove(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}