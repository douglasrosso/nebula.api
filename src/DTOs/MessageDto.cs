using System.ComponentModel.DataAnnotations;

namespace nebula.api.src.DTOs
{
    public class MessageDto
    {
        public string Id { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
    }

    public class SendMessageDto
    {
        [Required(ErrorMessage = "Mensagem é obrigatória.")]
        [MaxLength(2000, ErrorMessage = "Mensagem deve ter no máximo 2000 caracteres.")]
        public string Content { get; set; } = string.Empty;
    }
}
