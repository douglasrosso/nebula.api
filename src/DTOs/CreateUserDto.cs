using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres.")]
        public string? Password { get; set; }
    }
}
