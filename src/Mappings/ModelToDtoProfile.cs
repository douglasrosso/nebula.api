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
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

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
