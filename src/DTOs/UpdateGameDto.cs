using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class UpdateGameDto
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

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Requisitos do sistema são obrigatórios.")]
        public CreateSystemRequirementsDto SystemRequirements { get; set; } = new();
    }
}
