using nebula.api.src.Entities;
using nebula.api.src.DTOs;

namespace nebula.api.src.Repositories
{
    public interface IUserRepository
    {
        public Task<PaginatedResultDto<UserEntity>> Get(UserQueryDto query);
        public Task<UserEntity?> GetById(Guid id);
        public Task<UserEntity?> GetByEmail(string email);
        public Task<UserEntity> Create(UserEntity user);
    }
}
