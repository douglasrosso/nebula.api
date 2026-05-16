using AutoMapper;
using nebula.api.src.Common.DTOs;
using nebula.api.src.Common.Entities;
using nebula.api.src.Common.Repositories;
using nebula.api.src.DTOs;

namespace nebula.api.src.Common.Services
{
    public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto, TQuery>
        : IBaseService<TDto, TCreateDto, TUpdateDto, TQuery>
        where TEntity : BaseEntity
        where TQuery : BaseQueryDto
    {
        protected readonly IBaseRepository<TEntity, TQuery> _repository;
        protected readonly IMapper _mapper;

        protected BaseService(IBaseRepository<TEntity, TQuery> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<PaginatedResultDto<TDto>> Get(TQuery query)
        {
            var result = await _repository.Get(query);
            return new PaginatedResultDto<TDto>
            {
                Items = _mapper.Map<List<TDto>>(result.Items),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
        }

        public virtual async Task<TDto?> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            return entity is null ? default : _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> Create(TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await BeforeCreate(entity, dto);
            var created = await _repository.Create(entity);
            return _mapper.Map<TDto>(created);
        }

        public virtual async Task<TDto?> Update(Guid id, TUpdateDto dto)
        {
            var existing = await _repository.GetById(id);

            if (existing is null)
                return default;

            _mapper.Map(dto, existing);
            await BeforeUpdate(existing, dto);
            var updated = await _repository.Update(existing);
            return updated is null ? default : _mapper.Map<TDto>(updated);
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }

        protected virtual Task BeforeCreate(TEntity entity, TCreateDto dto) => Task.CompletedTask;
        protected virtual Task BeforeUpdate(TEntity entity, TUpdateDto dto) => Task.CompletedTask;
    }
}
