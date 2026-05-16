using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface ICartService
    {
        Task<List<CartItemDto>> GetCart(Guid userId);
        Task<CartItemDto> AddToCart(Guid userId, Guid gameId);
        Task<bool> RemoveFromCart(Guid userId, Guid gameId);
        Task ClearCart(Guid userId);
    }
}
