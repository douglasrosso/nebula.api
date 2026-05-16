using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nebula.api.src.Common.DTOs;
using nebula.api.src.Common.Services;
using nebula.api.src.DTOs;

namespace nebula.api.src.Common.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TService, TDto, TCreateDto, TUpdateDto, TQuery> : ControllerBase
        where TService : IBaseService<TDto, TCreateDto, TUpdateDto, TQuery>
        where TQuery : BaseQueryDto
    {
        protected readonly TService _service;

        protected BaseController(TService service)
        {
            _service = service;
        }

        private string ResourceName =>
            typeof(TDto).Name.Replace("Dto", string.Empty);

        [HttpGet]
        public virtual async Task<ActionResult<PaginatedResultDto<TDto>>> Get([FromQuery] TQuery query)
        {
            var result = await _service.Get(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<TDto>> GetById(Guid id)
        {
            var item = await _service.GetById(id);

            if (item is null)
                return NotFound(new { message = $"{ResourceName} não encontrado." });

            return Ok(item);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateDto dto)
        {
            var created = await _service.Create(dto);
            var id = created?.GetType().GetProperty("Id")?.GetValue(created);

            if (id is not null)
                return CreatedAtAction(nameof(GetById), new { id }, created);

            return StatusCode(201, created);
        }

        [HttpPut("{id:guid}")]
        public virtual async Task<ActionResult<TDto>> Update(Guid id, [FromBody] TUpdateDto dto)
        {
            var updated = await _service.Update(id, dto);

            if (updated is null)
                return NotFound(new { message = $"{ResourceName} não encontrado." });

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound(new { message = $"{ResourceName} não encontrado." });

            return NoContent();
        }
    }
}
