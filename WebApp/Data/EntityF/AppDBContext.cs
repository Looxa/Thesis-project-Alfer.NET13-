using FileSharer.ClassLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileSharer.Web.Data.EntityF
{
    public class AppDBContext : DbContext
    {
        public DbSet<Data.EntityF.File> Files { get; set; } = null!;
        public DbSet<Data.EntityF.User> Users { get; set; } = null!;
        //public DbSet<Data.EntityF.AccountInfo> AccountInformation { get; set; } = null!;   // добавить 
        public DbSet<Data.EntityF.Role> Roles { get; set; } = null!;            

        // ДОБАВИТЬ ТАБЛИЦУ ОТНОШЕНИЙ ПОЛЬЗОВАТЕЛЯ И ФАЙЛА
        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {

            Database.EnsureCreated();
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
           new Role[]
           { new Role{
               Id = 1,
               RoleName = "Admin"
            }
           });
            modelBuilder.Entity<User>().HasData(
            new User[]
            { new User{
                UserId = 1,
                FirstName = "Александр",
                LastName = "Сергеев",
                Email = "1@gmail.com",
                Avatar = "MOCK",
                RoleId = 1
            }
            });
        }
    }
}

