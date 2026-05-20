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
            entity.Username = string.IsNullOrWhiteSpace(dto.Username)
                ? normalizedEmail.Split('@')[0]
                : dto.Username.Trim().ToLowerInvariant();
            entity.DisplayName = dto.Name!.Trim();
        }

        protected override Task BeforeUpdate(UserEntity entity, UpdateUserDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name))
                entity.Name = dto.Name.Trim();

            if (!string.IsNullOrWhiteSpace(dto.Username))
                entity.Username = dto.Username.Trim().ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(dto.DisplayName))
                entity.DisplayName = dto.DisplayName.Trim();

            if (dto.Avatar is not null)
                entity.Avatar = dto.Avatar;

            if (dto.Country is not null)
                entity.Country = dto.Country.Trim();

            if (dto.Bio is not null)
                entity.Bio = dto.Bio.Trim();

            return Task.CompletedTask;
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
