using nebula.api.src.Common.DTOs;
using nebula.api.src.DTOs;

namespace nebula.api.src.Common.Services
{
    public interface IBaseService<TDto, TCreateDto, TUpdateDto, TQuery>
        where TQuery : BaseQueryDto
    {
        Task<PaginatedResultDto<TDto>> Get(TQuery query);
        Task<TDto?> GetById(Guid id);
        Task<TDto> Create(TCreateDto dto);
        Task<TDto?> Update(Guid id, TUpdateDto dto);
        Task<bool> Delete(Guid id);
    }
}
