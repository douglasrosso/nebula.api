using nebula.api.src.Common.Entities;

namespace nebula.api.src.Entities
{
    public class GameEntity : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int? Discount { get; set; }
        public string CoverImage { get; set; } = string.Empty;
        public string[] Screenshots { get; set; } = [];
        public string Developer { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; }
        public string[] Tags { get; set; } = [];
        public string[] Features { get; set; } = [];
        public SystemRequirements SystemRequirements { get; set; } = new();
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
        public int PositivePercentage { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<GameGenreEntity> GameGenres { get; set; } = [];
        public ICollection<ReviewEntity> Reviews { get; set; } = [];
        public ICollection<UserLibraryEntity> LibraryEntries { get; set; } = [];
        public ICollection<WishlistItemEntity> WishlistItems { get; set; } = [];
    }
}
