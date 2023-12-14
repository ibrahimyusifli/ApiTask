using Api202.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api202.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }
    }
}
