using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> Get();
    }
}