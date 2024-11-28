using CRUD_ADVANCE.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_ADVANCE.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
           base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Productss>().HasIndex(x => x.Name).IsUnique();
        }
        public DbSet<Productss> productss { get; set; }
    }
}
