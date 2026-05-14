using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher<UserEntity> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<PaginatedResultDto<UserDto>> Get(UserQueryDto query)
        {
            var users = await _userRepository.Get(query);
            var userModels = _mapper.Map<List<UserModel>>(users.Items);

            return new PaginatedResultDto<UserDto>
            {
                Items = _mapper.Map<List<UserDto>>(userModels),
                Page = users.Page,
                PageSize = users.PageSize,
                TotalItems = users.TotalItems,
                TotalPages = users.TotalPages
            };
        }

        public async Task<UserDto?> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);

            if (user is null)
                return null;

            var userModel = _mapper.Map<UserModel>(user);

            return _mapper.Map<UserDto>(userModel);
        }

        public async Task<UserDto> Create(CreateUserDto dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLowerInvariant();
            var existingUser = await _userRepository.GetByEmail(normalizedEmail);

            if (existingUser is not null)
                throw new InvalidOperationException("Email already registered.");

            var now = DateTime.UtcNow;
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = dto.Name.Trim(),
                Email = normalizedEmail,
                CreatedAt = now,
                UpdatedAt = now
            };

            user.Password = _passwordHasher.HashPassword(user, dto.Password);

            var createdUser = await _userRepository.Create(user);
            var userModel = _mapper.Map<UserModel>(createdUser);

            return _mapper.Map<UserDto>(userModel);
        }

        public async Task<UserDto?> Authenticate(LoginDto dto)
        {
            var user = await _userRepository.GetByEmail(dto.Email.Trim().ToLowerInvariant());

            if (user is null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            var userModel = _mapper.Map<UserModel>(user);

            return _mapper.Map<UserDto>(userModel);
        }
    }
}
