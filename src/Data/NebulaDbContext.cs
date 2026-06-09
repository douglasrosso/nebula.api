using Microsoft.EntityFrameworkCore;
using nebula.api.src.Entities;

namespace nebula.api.src.Data
{
    public class NebulaDbContext : DbContext
    {
        public NebulaDbContext(DbContextOptions<NebulaDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<GameEntity> Games => Set<GameEntity>();
        public DbSet<GenreEntity> Genres => Set<GenreEntity>();
        public DbSet<GameGenreEntity> GameGenres => Set<GameGenreEntity>();
        public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
        public DbSet<UserLibraryEntity> UserLibrary => Set<UserLibraryEntity>();
        public DbSet<WishlistItemEntity> Wishlist => Set<WishlistItemEntity>();
        public DbSet<FriendshipEntity> Friendships => Set<FriendshipEntity>();
        public DbSet<MessageEntity> Messages => Set<MessageEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.Price)
                .HasColumnType("numeric(10,2)");

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.OriginalPrice)
                .HasColumnType("numeric(10,2)");

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.Rating)
                .HasColumnType("numeric(3,2)");

            modelBuilder.Entity<GameEntity>()
                .OwnsOne(g => g.SystemRequirements, b =>
                {
                    b.ToJson();
                    b.OwnsOne(sr => sr.Minimum);
                    b.OwnsOne(sr => sr.Recommended);
                });

            modelBuilder.Entity<GameGenreEntity>()
                .HasKey(gg => new { gg.GameId, gg.GenreId });

            modelBuilder.Entity<GameGenreEntity>()
                .HasOne(gg => gg.Game)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GameId);

            modelBuilder.Entity<GameGenreEntity>()
                .HasOne(gg => gg.Genre)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GenreId);

            modelBuilder.Entity<ReviewEntity>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReviewEntity>()
                .HasOne(r => r.Game)
                .WithMany(g => g.Reviews)
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLibraryEntity>()
                .HasKey(l => new { l.UserId, l.GameId });

            modelBuilder.Entity<UserLibraryEntity>()
                .HasOne(l => l.User)
                .WithMany(u => u.Library)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<UserLibraryEntity>()
                .HasOne(l => l.Game)
                .WithMany(g => g.LibraryEntries)
                .HasForeignKey(l => l.GameId);

            modelBuilder.Entity<WishlistItemEntity>()
                .HasKey(w => new { w.UserId, w.GameId });

            modelBuilder.Entity<WishlistItemEntity>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlist)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<WishlistItemEntity>()
                .HasOne(w => w.Game)
                .WithMany(g => g.WishlistItems)
                .HasForeignKey(w => w.GameId);

            modelBuilder.Entity<FriendshipEntity>()
                .HasOne(f => f.Requester)
                .WithMany(u => u.SentRequests)
                .HasForeignKey(f => f.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendshipEntity>()
                .HasOne(f => f.Receiver)
                .WithMany(u => u.ReceivedRequests)
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
