using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [MaxLength(256, ErrorMessage = "Email deve ter no máximo 256 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres.")]
        [MaxLength(100, ErrorMessage = "Senha deve ter no máximo 100 caracteres.")]
        public string Password { get; set; } = string.Empty;
    }
}
