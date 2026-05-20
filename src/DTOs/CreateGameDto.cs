using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class CreateGameDto
    {
        [Required(ErrorMessage = "Título é obrigatório.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória.")]
        public string? Description { get; set; }

        public string? LongDescription { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Preço inválido.")]
        public decimal Price { get; set; }

        public decimal? OriginalPrice { get; set; }
        public int? Discount { get; set; }
        public string? CoverImage { get; set; }
        public string[] Screenshots { get; set; } = [];
        public string? Developer { get; set; }
        public string? Publisher { get; set; }
        public string? ReleaseDate { get; set; }
        public string[] GenreNames { get; set; } = [];
        public string[] Tags { get; set; } = [];
        public string[] Features { get; set; } = [];
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
        public int PositivePercentage { get; set; }
        public CreateSystemRequirementsDto SystemRequirements { get; set; } = new();
    }

    public class CreateSystemRequirementsDto
    {
        public CreateSystemRequirementSpecDto Minimum { get; set; } = new();
        public CreateSystemRequirementSpecDto Recommended { get; set; } = new();
    }

    public class CreateSystemRequirementSpecDto
    {
        public string? Os { get; set; }
        public string? Processor { get; set; }
        public string? Memory { get; set; }
        public string? Graphics { get; set; }
        public string? Storage { get; set; }
    }
}
