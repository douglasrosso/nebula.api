using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IWishlistService
    {
        Task<List<WishlistItemDto>> GetWishlist(Guid userId);
        Task<WishlistItemDto> AddToWishlist(Guid userId, Guid gameId);
        Task<bool> RemoveFromWishlist(Guid userId, Guid gameId);
    }
}
