using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NebulaDbContext _context;

        public UserRepository(NebulaDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResultDto<UserEntity>> Get(UserQueryDto query)
        {
            var usersQuery = _context.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var search = $"%{query.Search.Trim()}%";
                usersQuery = usersQuery.Where(user =>
                    EF.Functions.ILike(user.Name, search) ||
                    EF.Functions.ILike(user.Email, search));
            }

            var isDescending = query.SortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase);

            usersQuery = query.SortBy.Trim().ToLowerInvariant() switch
            {
                "name" => isDescending
                    ? usersQuery.OrderByDescending(user => user.Name)
                    : usersQuery.OrderBy(user => user.Name),
                "email" => isDescending
                    ? usersQuery.OrderByDescending(user => user.Email)
                    : usersQuery.OrderBy(user => user.Email),
                "updatedat" => isDescending
                    ? usersQuery.OrderByDescending(user => user.UpdatedAt)
                    : usersQuery.OrderBy(user => user.UpdatedAt),
                _ => isDescending
                    ? usersQuery.OrderByDescending(user => user.CreatedAt)
                    : usersQuery.OrderBy(user => user.CreatedAt)
            };

            var totalItems = await usersQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);
            var users = await usersQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PaginatedResultDto<UserEntity>
            {
                Items = users,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<UserEntity?> GetById(Guid id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            var normalizedEmail = email.Trim().ToLower();

            return await _context.Users.FirstOrDefaultAsync(user => user.Email == normalizedEmail);
        }

        public async Task<UserEntity> Create(UserEntity user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
