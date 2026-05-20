using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface ILibraryService
    {
        Task<List<LibraryItemDto>> GetLibrary(Guid userId);
        Task AddToLibrary(Guid userId, Guid gameId, DateTime? acquiredAt = null);
        Task<bool> IsInLibrary(Guid userId, Guid gameId);
        Task<int> CountByUser(Guid userId);
    }
}
