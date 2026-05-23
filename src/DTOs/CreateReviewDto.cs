using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class CreateReviewDto
    {
        [Required(ErrorMessage = "ID do jogo é obrigatório.")]
        public Guid GameId { get; set; }

        [Required(ErrorMessage = "Avaliação é obrigatória.")]
        [RegularExpression(@"^(positive|negative)$", ErrorMessage = "Avaliação deve ser 'positive' ou 'negative'.")]
        public string Rating { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Horas jogadas inválidas.")]
        public int HoursPlayed { get; set; }

        [Required(ErrorMessage = "Conteúdo é obrigatório.")]
        [MinLength(10, ErrorMessage = "Review deve ter pelo menos 10 caracteres.")]
        [MaxLength(5000, ErrorMessage = "Review deve ter no máximo 5000 caracteres.")]
        public string Content { get; set; } = string.Empty;
    }
}
