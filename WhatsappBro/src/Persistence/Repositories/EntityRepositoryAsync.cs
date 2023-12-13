using Application.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public abstract class EntityRepositoryAsync<TEntity> : IEntityRepositoryAsync<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected internal DbContext _context;
        protected internal DbSet<TEntity> _entity;
        public EntityRepositoryAsync(DbContext dbContext)
        {
            _context = dbContext;
            _entity = _context.Set<TEntity>();
        }
        public async Task<List<TEntity>> GetsAsync(Expression<Func<TEntity, bool>>? filter)
        {
            return filter is null ? await _entity.ToListAsync() : await _entity.Where(filter).ToListAsync();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entity.SingleOrDefaultAsync(filter);
        }
        public async Task InsertAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }
    }
}
