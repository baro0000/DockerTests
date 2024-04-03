using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SOTags.DataAccess
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseDbContext>
    {
        public DatabaseDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<DatabaseDbContext>();
            optionBuilder.UseSqlServer("Data Source=DESKTOP-CBK7MCF\\SQLEXPRESS;Initial Catalog=StackOverflowTags;Integrated Security=True;Trust Server Certificate=True");
            //optionBuilder.UseSqlServer("Data Source=DESKTOP-N79UBH8\\SQLEXPRESS;Initial Catalog=StackOverflowTags;Integrated Security=True;Trust Server Certificate=True");
            return new DatabaseDbContext(optionBuilder.Options);
        }
    }
}
