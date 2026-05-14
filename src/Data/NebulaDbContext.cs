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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(user => user.Email)
                .IsUnique();
        }
    }
}
