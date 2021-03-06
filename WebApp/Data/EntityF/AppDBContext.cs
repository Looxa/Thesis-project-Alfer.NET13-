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
           {
               new Role{Id = 1, RoleName = "Admin"},
               new Role{Id = 2, RoleName = "User"},
               new Role{Id = 3, RoleName = "Guest"}
           });
            modelBuilder.Entity<User>().HasData(
            new User[]
            { new User{
                UserId = 1,
                Password = "1",
                FirstName = "Александр",
                LastName = "Сергеев",
                Email = "1@gmail.com",
                //Avatar = "MOCK",
                RoleId = 1},
              new User
              {
                UserId = 2,
                Password = "2",
                FirstName = "Стас",
                LastName = "Гаврилов",
                Email = "2@gmail.com",
                //Avatar = "MOCK",
                RoleId = 2},
              new User
              {
                UserId = 3,
                Password = "0",
                FirstName = "",
                LastName = "",
                Email = "0@gmail.com",
                //Avatar = "MOCK",
                RoleId = 3},

            });

        }
    }
}

