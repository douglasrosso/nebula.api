using AutoMapper;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Models;

namespace nebula.api.src.Mappings
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<UserModel, UserDto>()
                .ForMember(dest => dest.Username, opt => opt.Ignore())
                .ForMember(dest => dest.DisplayName, opt => opt.Ignore())
                .ForMember(dest => dest.Avatar, opt => opt.Ignore())
                .ForMember(dest => dest.Level, opt => opt.Ignore())
                .ForMember(dest => dest.Xp, opt => opt.Ignore())
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.Bio, opt => opt.Ignore())
                .ForMember(dest => dest.FriendCount, opt => opt.Ignore())
                .ForMember(dest => dest.GamesOwned, opt => opt.Ignore())
                .ForMember(dest => dest.Badges, opt => opt.Ignore());
            CreateMap<UserDto, UserModel>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.GamesOwned, opt => opt.MapFrom(src => src.Library.Count));

            CreateMap<GameEntity, GameSummaryDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                    src.GameGenres.Select(gg => gg.Genre.Name).ToArray()));

            CreateMap<GameEntity, GameDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                    src.GameGenres.Select(gg => gg.Genre.Name).ToArray()))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src =>
                    src.ReleaseDate.ToString("yyyy-MM-dd")));

            CreateMap<SystemRequirements, SystemRequirementsDto>();
            CreateMap<SystemRequirementSpec, SystemRequirementSpecDto>();

            CreateMap<GenreEntity, GenreDto>();
        }
    }
}
