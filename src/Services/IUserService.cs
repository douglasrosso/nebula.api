using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IUserService
    {
        public Task<PaginatedResultDto<UserDto>> Get(UserQueryDto query);
        public Task<UserDto?> GetById(Guid id);
        public Task<UserDto> Create(CreateUserDto dto);
        public Task<UserDto?> Authenticate(LoginDto dto);
    }
}
