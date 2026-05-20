using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MinLength(2, ErrorMessage = "Nome deve ter pelo menos 2 caracteres.")]
        public string? Name { get; set; }

        public string? Username { get; set; }
        public string? DisplayName { get; set; }
        public string? Avatar { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
    }
}
