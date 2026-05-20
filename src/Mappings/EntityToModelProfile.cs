using AutoMapper;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Models;

namespace nebula.api.src.Mappings
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<UserModel, UserEntity>().ReverseMap();

            CreateMap<CreateUserDto, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<UpdateUserDto, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<CreateGameDto, GameEntity>()
                .ForMember(dest => dest.ReleaseDate, opt => opt.Ignore())
                .ForMember(dest => dest.GameGenres, opt => opt.Ignore())
                .ForMember(dest => dest.SystemRequirements, opt => opt.MapFrom(src => src.SystemRequirements));

            CreateMap<UpdateGameDto, GameEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ReleaseDate, opt => opt.Ignore())
                .ForMember(dest => dest.GameGenres, opt => opt.Ignore())
                .ForMember(dest => dest.ReviewCount, opt => opt.Ignore())
                .ForMember(dest => dest.PositivePercentage, opt => opt.Ignore())
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.SystemRequirements, opt => opt.MapFrom(src => src.SystemRequirements));

            CreateMap<CreateSystemRequirementsDto, SystemRequirements>()
                .ForMember(dest => dest.Minimum, opt => opt.MapFrom(src => src.Minimum))
                .ForMember(dest => dest.Recommended, opt => opt.MapFrom(src => src.Recommended));

            CreateMap<CreateSystemRequirementSpecDto, SystemRequirementSpec>();
        }
    }
}
