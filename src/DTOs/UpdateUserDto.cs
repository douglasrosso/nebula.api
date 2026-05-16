using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MinLength(2, ErrorMessage = "Nome deve ter pelo menos 2 caracteres.")]
        public string? Name { get; set; }
    }
}
