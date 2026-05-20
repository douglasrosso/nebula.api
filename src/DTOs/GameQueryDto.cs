using nebula.api.src.Common.DTOs;

namespace nebula.api.src.DTOs
{
    public class GameQueryDto : BaseQueryDto
    {
        public string[]? Genres { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
