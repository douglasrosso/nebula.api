using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
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

        public async Task<IEnumerable<UserEntity>> Get()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
