using nebula.api.src.Common.Repositories;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories
{
    public interface IUserRepository : IBaseRepository<UserEntity, UserQueryDto>
    {
        Task<UserEntity?> GetByEmail(string email);
    }
}
