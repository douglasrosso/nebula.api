using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class CreateGameDto
    {
        [Required(ErrorMessage = "Título é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Título deve ter no máximo 200 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição é obrigatória.")]
        [MaxLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres.")]
        public string Description { get; set; } = string.Empty;

        [MaxLength(10000, ErrorMessage = "Descrição longa deve ter no máximo 10000 caracteres.")]
        public string? LongDescription { get; set; }

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Preço inválido.")]
        public decimal Price { get; set; }

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Preço original inválido.")]
        public decimal? OriginalPrice { get; set; }

        [Range(0, 100, ErrorMessage = "Desconto deve ser entre 0 e 100.")]
        public int? Discount { get; set; }

        [Required(ErrorMessage = "Imagem de capa é obrigatória.")]
        [MaxLength(500, ErrorMessage = "URL da imagem deve ter no máximo 500 caracteres.")]
        public string CoverImage { get; set; } = string.Empty;

        public string[] Screenshots { get; set; } = [];

        [Required(ErrorMessage = "Desenvolvedor é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Desenvolvedor deve ter no máximo 200 caracteres.")]
        public string Developer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publicador é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Publicador deve ter no máximo 200 caracteres.")]
        public string Publisher { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data de lançamento é obrigatória.")]
        public string ReleaseDate { get; set; } = string.Empty;

        public string[] GenreNames { get; set; } = [];
        public string[] Tags { get; set; } = [];
        public string[] Features { get; set; } = [];

        [Range(0, 5, ErrorMessage = "Rating deve ser entre 0 e 5.")]
        public decimal Rating { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Contagem de reviews inválida.")]
        public int ReviewCount { get; set; }

        [Range(0, 100, ErrorMessage = "Percentual positivo deve ser entre 0 e 100.")]
        public int PositivePercentage { get; set; }

        [Required(ErrorMessage = "Requisitos do sistema são obrigatórios.")]
        public CreateSystemRequirementsDto SystemRequirements { get; set; } = new();
    }

    public class CreateSystemRequirementsDto
    {
        [Required(ErrorMessage = "Requisitos mínimos são obrigatórios.")]
        public CreateSystemRequirementSpecDto Minimum { get; set; } = new();

        [Required(ErrorMessage = "Requisitos recomendados são obrigatórios.")]
        public CreateSystemRequirementSpecDto Recommended { get; set; } = new();
    }

    public class CreateSystemRequirementSpecDto
    {
        [Required(ErrorMessage = "Sistema operacional é obrigatório.")]
        [MaxLength(200, ErrorMessage = "SO deve ter no máximo 200 caracteres.")]
        public string Os { get; set; } = string.Empty;

        [Required(ErrorMessage = "Processador é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Processador deve ter no máximo 200 caracteres.")]
        public string Processor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Memória é obrigatória.")]
        [MaxLength(50, ErrorMessage = "Memória deve ter no máximo 50 caracteres.")]
        public string Memory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Placa de vídeo é obrigatória.")]
        [MaxLength(200, ErrorMessage = "Placa de vídeo deve ter no máximo 200 caracteres.")]
        public string Graphics { get; set; } = string.Empty;

        [Required(ErrorMessage = "Armazenamento é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Armazenamento deve ter no máximo 100 caracteres.")]
        public string Storage { get; set; } = string.Empty;
    }
}
