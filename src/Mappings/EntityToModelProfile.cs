using AutoMapper;
using nebula.api.src.Entities;
using nebula.api.src.Models;

namespace nebula.api.src.Mappings
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<UserModel, UserEntity>()
                    .ReverseMap();
        }
    }
}