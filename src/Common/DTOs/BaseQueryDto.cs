using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.Common.DTOs
{
    public abstract class BaseQueryDto
    {
        private const int MaxPageSize = 100;
        private int _page = 1;
        private int _pageSize = 10;

        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value switch
            {
                < 1 => 10,
                > MaxPageSize => MaxPageSize,
                _ => value
            };
        }

        [MaxLength(100, ErrorMessage = "Busca deve ter no máximo 100 caracteres.")]
        public string? Search { get; set; }

        public string SortBy { get; set; } = "createdAt";

        [RegularExpression(@"^(asc|desc)$", ErrorMessage = "Direção de ordenação deve ser 'asc' ou 'desc'.")]
        public string SortDirection { get; set; } = "desc";
    }
}
