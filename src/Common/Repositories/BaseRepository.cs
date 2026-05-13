using Microsoft.EntityFrameworkCore;
using nebula.api.src.Common.DTOs;
using nebula.api.src.Common.Entities;
using nebula.api.src.DTOs;

namespace nebula.api.src.Common.Repositories
{
    public abstract class BaseRepository<TEntity, TQuery> : IBaseRepository<TEntity, TQuery>
        where TEntity : BaseEntity
        where TQuery : BaseQueryDto
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected abstract IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query, TQuery queryDto);

        public async Task<PaginatedResultDto<TEntity>> Get(TQuery query)
        {
            var filtered = ApplyFilters(_dbSet.AsNoTracking(), query);

            var totalItems = await filtered.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);
            var items = await filtered
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PaginatedResultDto<TEntity>
            {
                Items = items,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = entity.CreatedAt;

            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            var existing = await _dbSet.FindAsync(entity.Id);
            if (existing is null) return null;

            entity.CreatedAt = existing.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
