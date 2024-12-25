using TransactionService.Contract.Interfaces;

namespace TransactionService.Contract
{
    /// <summary>
    /// Индерфейс работы с данными
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : class, IEntity;

        /// <summary>
        /// Метод асинхронного сохранения
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        /// <summary>
        /// Асинхронное обновление.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        /// <summary>
        ///  Асинхронное удаление.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;
    }
}