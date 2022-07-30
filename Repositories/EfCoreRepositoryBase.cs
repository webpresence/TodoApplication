using Microsoft.EntityFrameworkCore;
using ToDoApplication.Repositories.Interfaces;
namespace ToDoApplication.Repositories
{

    public class EfCoreRepositoryBase<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
    where TDbContext : DbContext
    where TEntity : class
        {
            private readonly TDbContext _dbContext;

            public EfCoreRepositoryBase(TDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            protected TDbContext GetDbContext() => _dbContext;

            protected DbSet<TEntity> DbSet => GetDbContext().Set<TEntity>();

            public async Task Delete(TKey key)
            {
                var entity = await DbSet.FindAsync(key);
                if (entity is null)
                {
                    throw new  Exception($"{typeof(TEntity).Name} with key {key} not found");
                }

                DbSet.Remove(entity);
            }

            public async Task<bool> Exists(TKey key)
            {
                return (await DbSet.FindAsync(key)) != null;
            }

            public async Task<TEntity> Get(TKey key)
            {
                return await DbSet.FindAsync(key);
            }

            public async Task<IEnumerable<TEntity>> GetAll()
            {
                return await DbSet.ToListAsync();
            }

            public async Task<TEntity> Insert(TEntity entity)
            {
                return (await DbSet.AddAsync(entity)).Entity;
            }

            public async Task Insert(IEnumerable<TEntity> entities)
            {
                await DbSet.AddRangeAsync(entities);
            }

            public async Task Reload(TEntity entity)
            {
                await GetDbContext().Entry(entity).ReloadAsync();
            }

            public TEntity Update(TEntity entity)
            {
                return DbSet.Update(entity).Entity;
            }

            public int SaveChanges()
            {
               return GetDbContext().SaveChanges();
            }
        }
}
