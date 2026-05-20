using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class CreateReviewDto
    {
        [Required]
        public Guid GameId { get; set; }

        [Required(ErrorMessage = "Avaliação é obrigatória.")]
        public string? Rating { get; set; }

        [Range(0, int.MaxValue)]
        public int HoursPlayed { get; set; }

        [Required(ErrorMessage = "Conteúdo é obrigatório.")]
        [MinLength(10, ErrorMessage = "Review deve ter pelo menos 10 caracteres.")]
        public string? Content { get; set; }
    }
}
