using nebula.api.src.Common.DTOs;
using nebula.api.src.Common.Entities;
using nebula.api.src.DTOs;

namespace nebula.api.src.Common.Repositories
{
    public interface IBaseRepository<TEntity, TQuery>
        where TEntity : BaseEntity
        where TQuery : BaseQueryDto
    {
        Task<PaginatedResultDto<TEntity>> Get(TQuery query);
        Task<TEntity?> GetById(Guid id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity?> Update(TEntity entity);
        Task<bool> Delete(Guid id);
    }
}
