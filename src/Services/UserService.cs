using AutoMapper;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Models;
using nebula.api.src.Repositories;

namespace nebula.api.src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> Get()
        {
            List<UserEntity> userEntity = (await _userRepository.Get()).ToList();
            List<UserModel> UserDtos = _mapper.Map<List<UserModel>>(userEntity);
            return _mapper.Map<List<UserDto>>(UserDtos);
        }
    }
}