using nebula.api.src.Common.Services;
using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IUserService : IBaseService<UserDto, CreateUserDto, UpdateUserDto, UserQueryDto>
    {
        Task<UserDto?> Authenticate(LoginDto dto);
    }
}
