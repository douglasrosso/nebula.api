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
            CreateMap<UserDto, UserModel>().ReverseMap();

            // mapeamento direto Entity → Dto usado pela camada base
            CreateMap<UserEntity, UserDto>();
        }
    }
}