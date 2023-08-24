using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace NextGen.Contract.Repository
{
    public interface IRepository<T> where T : class
    {
        Task SaveChangesAsync();

        IQueryable<T> Query();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        Task AddAsync<TEntity>(TEntity entity) where TEntity : class;

        Task AddRangeAsync(IEnumerable<object> entities);

        void Remove<TEntity>(TEntity entity) where TEntity : class;

        void Update<TEntity>(TEntity entity);

        IDbContextTransaction BeginTransaction();

        int GenerateId(Expression<Func<T, int>> selector);
    }
}
