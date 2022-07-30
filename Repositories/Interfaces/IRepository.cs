namespace ToDoApplication.Repositories.Interfaces
{
    public interface IRepository
    {

    }

    public interface IRepository<TEntity, in TKey> : IRepository
        where TEntity : class
    {
        Task<bool> Exists(TKey key);

        Task<TEntity> Get(TKey key);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Insert(TEntity entity);

        Task Insert(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        Task Delete(TKey key);

        Task Reload(TEntity entity);

        int SaveChanges();
    }
}
