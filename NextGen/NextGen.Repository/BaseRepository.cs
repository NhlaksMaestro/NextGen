using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using NextGen.Contract.Repository;
using System.Linq.Expressions;

namespace NextGen.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>, IDisposable where T : class
    {
        protected readonly TaxCalculatorDbContext _db;
        private bool disposed;

        protected BaseRepository(TaxCalculatorDbContext dbContext) => _db = dbContext;

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            EntityEntry entityEntry = await _db.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<object> entities) => await _db.AddRangeAsync(entities);

        public void Remove<TEntity>(TEntity entity) where TEntity : class => _db.Remove(entity);

        public void Update<TEntity>(TEntity entity) => _db.Update(entity);

        public int GenerateId(Expression<Func<T, int>> selector) => Query().Any() ? 1 : Query().Select(selector).Max() + 1;

        public IQueryable<T> Query() => _db.Set<T>();

        public DbSet<TEntity> Set<TEntity>() where TEntity : class => _db.Set<TEntity>();

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class => _db.Set<TEntity>();

        public async Task SaveChangesAsync()
        {
            int num = await _db.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction() => _db.Database.BeginTransaction();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                _db.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}