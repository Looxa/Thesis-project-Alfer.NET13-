using FileSharer.ClassLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileSharer.Web.Data.EntityF
{
    public class AppDBContext : DbContext
    {
        public DbSet<Data.EntityF.File> Files { get; set; } = null!;
        public DbSet<Data.EntityF.User> Users { get; set; } = null!;
        //public DbSet<Data.EntityF.AccountInfo> AccountInformation { get; set; } = null!;   // добавить 
        //public DbSet<Data.EntityF.Role> Roles { get; set; } = null!;                       // добавить

        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();

        }
    }
}

