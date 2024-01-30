using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
    public class inherDbContext : DbContext
    {

        public inherDbContext(DbContextOptions dbContextOptions) :base(dbContextOptions)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("products").HasKey("Id");

        }
















    }
}
