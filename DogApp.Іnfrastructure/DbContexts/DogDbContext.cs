using DogApp.Domain.DbEntities;
using DogApp.Іnfrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DogApp.Іnfrastructure.DbContexts
{
    public class DogDbContext : DbContext
    {
        public DbSet<DbDog> Dogs { get; set; }

        public DogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DogEntityTypeConfiguration());
        }
    }
}
