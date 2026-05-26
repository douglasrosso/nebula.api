using nebula.api.src.Common.DTOs;

namespace nebula.api.src.DTOs
{
    public class ReviewQueryDto : BaseQueryDto
    {
        public Guid? GameId { get; set; }
        public Guid? UserId { get; set; }
    }
}
