using Microsoft.EntityFrameworkCore;
using nebula.api.src.Common.Repositories;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories
{
    public class UserRepository : BaseRepository<UserEntity, UserQueryDto>, IUserRepository
    {
        public UserRepository(NebulaDbContext context) : base(context) { }

        protected override IQueryable<UserEntity> ApplyFilters(IQueryable<UserEntity> query, UserQueryDto queryDto)
        {
            if (!string.IsNullOrWhiteSpace(queryDto.Search))
            {
                var search = $"%{queryDto.Search.Trim()}%";
                query = query.Where(user =>
                    EF.Functions.ILike(user.Name, search) ||
                    EF.Functions.ILike(user.Email, search));
            }

            var isDescending = queryDto.SortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase);

            query = queryDto.SortBy.Trim().ToLowerInvariant() switch
            {
                "name" => isDescending
                    ? query.OrderByDescending(user => user.Name)
                    : query.OrderBy(user => user.Name),
                "email" => isDescending
                    ? query.OrderByDescending(user => user.Email)
                    : query.OrderBy(user => user.Email),
                "updatedat" => isDescending
                    ? query.OrderByDescending(user => user.UpdatedAt)
                    : query.OrderBy(user => user.UpdatedAt),
                _ => isDescending
                    ? query.OrderByDescending(user => user.CreatedAt)
                    : query.OrderBy(user => user.CreatedAt)
            };

            return query;
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            var normalizedEmail = email.Trim().ToLower();
            return await _dbSet.FirstOrDefaultAsync(user => user.Email == normalizedEmail);
        }
    }
}
