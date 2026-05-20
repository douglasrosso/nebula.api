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
        public DbSet<CartItemEntity> Cart => Set<CartItemEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<OrderItemEntity> OrderItems => Set<OrderItemEntity>();

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

            modelBuilder.Entity<CartItemEntity>()
                .HasKey(c => new { c.UserId, c.GameId });

            modelBuilder.Entity<CartItemEntity>()
                .HasOne(c => c.User)
                .WithMany(u => u.Cart)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<CartItemEntity>()
                .HasOne(c => c.Game)
                .WithMany(g => g.CartItems)
                .HasForeignKey(c => c.GameId);

            modelBuilder.Entity<OrderEntity>()
                .Property(o => o.TotalAmount)
                .HasColumnType("numeric(10,2)");

            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<OrderItemEntity>()
                .HasKey(i => new { i.OrderId, i.GameId });

            modelBuilder.Entity<OrderItemEntity>()
                .Property(i => i.PricePaid)
                .HasColumnType("numeric(10,2)");

            modelBuilder.Entity<OrderItemEntity>()
                .HasOne(i => i.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OrderId);

            modelBuilder.Entity<OrderItemEntity>()
                .HasOne(i => i.Game)
                .WithMany(g => g.OrderItems)
                .HasForeignKey(i => i.GameId);
        }
    }
}
