using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [MaxLength(256, ErrorMessage = "Email deve ter no máximo 256 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres.")]
        [MaxLength(100, ErrorMessage = "Senha deve ter no máximo 100 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Username deve ter no máximo 50 caracteres.")]
        public string? Username { get; set; }
    }
}
