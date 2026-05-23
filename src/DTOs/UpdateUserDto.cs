using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MinLength(2, ErrorMessage = "Nome deve ter pelo menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [MinLength(3, ErrorMessage = "Username deve ter pelo menos 3 caracteres.")]
        [MaxLength(50, ErrorMessage = "Username deve ter no máximo 50 caracteres.")]
        public string? Username { get; set; }

        [MaxLength(100, ErrorMessage = "Nome de exibição deve ter no máximo 100 caracteres.")]
        public string? DisplayName { get; set; }

        [MaxLength(500, ErrorMessage = "URL do avatar deve ter no máximo 500 caracteres.")]
        public string? Avatar { get; set; }

        [MaxLength(100, ErrorMessage = "País deve ter no máximo 100 caracteres.")]
        public string? Country { get; set; }

        [MaxLength(500, ErrorMessage = "Bio deve ter no máximo 500 caracteres.")]
        public string? Bio { get; set; }
    }
}
