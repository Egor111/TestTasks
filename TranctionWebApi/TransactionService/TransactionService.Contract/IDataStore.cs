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
        /// Получение данных по идентификатору
        /// </summary>
        /// <param name="id"></param>
        TEntity Get<TEntity>(long id)
            where TEntity : class, IEntity;

        /// <summary>
        /// Асинхронное получение данных по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        Task<TEntity> GetAsync<TEntity>(long id, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        /// <summary>
        /// Метод синхронного сохоанения
        /// </summary>
        /// <param name="entity"></param>
        void Save<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Метод асинхронного сохранения
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        /// <summary>
        /// Метод массового сохранения.
        /// </summary>
        /// <param name="entities"></param>
        void Save<TEntity>(List<TEntity> entities) where TEntity : class, IEntity;

        /// <summary>
        /// Метод массового асинхронного сохранения.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        Task SaveAsync<TEntity>(List<TEntity> entities,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        /// <summary>
        /// Синхронное обновление.
        /// </summary>
        /// <param name="entity"></param>
        void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        /// <summary>
        /// Массовое синхронное обновление.
        /// </summary>
        /// <param name="entity"></param>
        void Update<TEntity>(List<TEntity> entities)
            where TEntity : class, IEntity;

        /// <summary>
        /// Асинхронное обновление.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        /// <summary>
        /// Массовое асинхронное обновление.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        /// <summary>
        ///  Синхронное удаление.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(IEntity entity);

        /// <summary>
        ///  Массовое синхронное удаление.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(List<IEntity> entities);

        /// <summary>
        ///  Асинхронное удаление.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;

        /// <summary>
        ///  Массовое асинхронное удаление.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync<TEntity>(List<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity;
    }
}