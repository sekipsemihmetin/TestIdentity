using TestIdentity.Domain.Core.BaseEntites;
using TestIdentity.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Infrastructure.DataAccess.EntityFramework
{
    public class EFBaseRepository<TEntity> : IAsyncDeletableRepository<TEntity>, IAsyncFindableRepository<TEntity>, IAsyncInsertable<TEntity>, IAsyncOrderableRepository<TEntity>, IAsyncQueryableRepository<TEntity>, IAsyncRepository, IAsyncTransactionRepository, IRepository, IAsyncUpdatableRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _table;

        public EFBaseRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsnyc(TEntity entity)
        {
            var entry = await _table.AddAsync(entity);
            return entry.Entity;
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return _table.AddRangeAsync(entities);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression is null ? GetAllEntites().AnyAsync() : GetAllEntites().AnyAsync(expression);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public Task<IExecutionStrategy> CreateExecutionStrategy()
        {
            return Task.FromResult(_context.Database.CreateExecutionStrategy());
        }

        public Task DeleteAsync(TEntity entity)
        {
            return Task.FromResult(_table.Remove(entity));
        }

        public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
            return _context.SaveChangesAsync();
        }

        

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
        {
            return await GetAllEntites(tracking).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            return await GetAllEntites(tracking).Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true)
        {
            return orderByDesc ? await GetAllEntites(tracking).OrderByDescending(orderBy).ToListAsync() : await GetAllEntites(tracking).OrderBy(orderBy).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true)
        {
            var values = GetAllEntites(tracking).Where(expression);
            return orderByDesc ? await values.OrderByDescending(orderBy).ToListAsync() : await values.OrderBy(orderBy).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            return await GetAllEntites(tracking).FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
        {
            return await GetAllEntites(tracking).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<int> SaveChangeAsync()
        {
            return _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.FromResult(_table.Update(entity).Entity);

        }
        protected IQueryable<TEntity> GetAllEntites(bool tracking = true)
        {
            var values = _table.Where(x => x.Status != Domain.Enums.Status.Deleted);
            return tracking ? values : values.AsNoTracking();
        }
    }
}
