using nebula.api.src.Common.Entities;

namespace nebula.api.src.Entities
{
    public class GenreEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

        public ICollection<GameGenreEntity> GameGenres { get; set; } = [];
    }
}
