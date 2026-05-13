using nebula.api.src.Entities;

namespace nebula.api.src.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserEntity>> Get();
    }
}