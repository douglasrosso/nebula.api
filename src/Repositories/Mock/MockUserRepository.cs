using nebula.api.src.Common.Repositories;
using nebula.api.src.Data.Mock;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories.Mock
{
    public class MockUserRepository : IUserRepository
    {
        private static readonly List<UserEntity> _store = MockData.Users;

        public Task<PaginatedResultDto<UserEntity>> Get(UserQueryDto query)
        {
            var items = _store.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query.Search))
                items = items.Where(u =>
                    u.Name.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                    u.Username.Contains(query.Search, StringComparison.OrdinalIgnoreCase));

            items = (query.SortBy.ToLower(), query.SortDirection.ToLower()) switch
            {
                ("name",  "asc") => items.OrderBy(u => u.Name),
                ("name",  _)     => items.OrderByDescending(u => u.Name),
                ("email", "asc") => items.OrderBy(u => u.Email),
                ("email", _)     => items.OrderByDescending(u => u.Email),
                (_,       "asc") => items.OrderBy(u => u.CreatedAt),
                _                => items.OrderByDescending(u => u.CreatedAt),
            };

            var list = items.ToList();
            var total = list.Count;

            return Task.FromResult(new PaginatedResultDto<UserEntity>
            {
                Items      = list.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize),
                Page       = query.Page,
                PageSize   = query.PageSize,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling((double)total / query.PageSize),
            });
        }

        public Task<UserEntity?> GetById(Guid id) =>
            Task.FromResult(_store.FirstOrDefault(u => u.Id == id));

        public Task<UserEntity?> GetByEmail(string email) =>
            Task.FromResult(_store.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));

        public Task<UserEntity> Create(UserEntity entity)
        {
            entity.Id        = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            _store.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<UserEntity?> Update(UserEntity entity)
        {
            var existing = _store.FirstOrDefault(u => u.Id == entity.Id);
            if (existing is null) return Task.FromResult<UserEntity?>(null);

            existing.Name        = entity.Name;
            existing.Email       = entity.Email;
            existing.Username    = entity.Username;
            existing.DisplayName = entity.DisplayName;
            existing.Avatar      = entity.Avatar;
            existing.Bio         = entity.Bio;
            existing.Country     = entity.Country;
            existing.Password    = entity.Password;
            existing.Xp          = entity.Xp;
            existing.Level       = entity.Level;
            existing.Badges      = entity.Badges;
            existing.UpdatedAt   = DateTime.UtcNow;

            return Task.FromResult<UserEntity?>(existing);
        }

        public Task<bool> Delete(Guid id)
        {
            var entity = _store.FirstOrDefault(u => u.Id == id);
            if (entity is null) return Task.FromResult(false);
            _store.Remove(entity);
            return Task.FromResult(true);
        }
    }
}
