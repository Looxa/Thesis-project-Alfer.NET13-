using FileSharer.ClassLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileSharer.Web.Data.EntityF
{
    public class AppDBContext : DbContext
    {
        public DbSet<Data.EntityF.File> Files { get; set; } = null!;

        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();

        }
    }
}

