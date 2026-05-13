namespace nebula.api.src.Entities
{
    public class GameGenreEntity
    {
        public Guid GameId { get; set; }
        public Guid GenreId { get; set; }

        public GameEntity Game { get; set; } = null!;
        public GenreEntity Genre { get; set; } = null!;
    }
}
