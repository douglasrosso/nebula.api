using System.ComponentModel.DataAnnotations;
using nebula.api.src.Common.DTOs;

namespace nebula.api.src.DTOs
{
    public class GameQueryDto : BaseQueryDto
    {
        public string[]? Genres { get; set; }

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Preço mínimo inválido.")]
        public decimal? MinPrice { get; set; }

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Preço máximo inválido.")]
        public decimal? MaxPrice { get; set; }
    }
}
