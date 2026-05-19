using AutoMapper;
using Microsoft.AspNetCore.Identity;
using nebula.api.src.Common.Services;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Repositories;

namespace nebula.api.src.Services
{
    public class UserService
        : BaseService<UserEntity, UserDto, CreateUserDto, UpdateUserDto, UserQueryDto>,
          IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher<UserEntity> passwordHasher)
            : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        protected override async Task BeforeCreate(UserEntity entity, CreateUserDto dto)
        {
            var normalizedEmail = dto.Email!.Trim().ToLowerInvariant();
            var existing = await _userRepository.GetByEmail(normalizedEmail);

            if (existing is not null)
                throw new InvalidOperationException("Email already registered.");

            entity.Name = dto.Name!.Trim();
            entity.Email = normalizedEmail;
            entity.Password = _passwordHasher.HashPassword(entity, dto.Password!);
        }

        public async Task<UserDto?> Authenticate(LoginDto dto)
        {
            var user = await _userRepository.GetByEmail(dto.Email!.Trim().ToLowerInvariant());

            if (user is null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password!);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return _mapper.Map<UserDto>(user);
        }
    }
}
