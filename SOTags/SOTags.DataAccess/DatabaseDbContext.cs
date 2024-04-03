using Microsoft.EntityFrameworkCore;
using SOTags.DataAccess.Entities;

namespace SOTags.DataAccess
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }
    }
}
