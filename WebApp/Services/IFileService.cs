using FileSharer.Web.Data.EntityF;
using FileSharer.Web.Models;
using File = FileSharer.Web.Data.EntityF.File;

namespace FileSharer.Web.Services
{
    public interface IFileService
    {
        public IEnumerable<FileSharer.ClassLibrary.Entities.File> GetAll();
        
        public void Add(string fileName, string filePath);

        public void Delete(int id);
        string GetFilePath(int? id);
        string GetFileType(int? id);
        bool NullOrNot();
        FileModel GetNullFile(FileSharer.ClassLibrary.Entities.File file);
        IEnumerable<FileSharer.ClassLibrary.Entities.User> GetAllUsers();
        ClassLibrary.Entities.User UsersDBToEntity(int? id);

        User GetUser(int? id);
        void DeleteUser(User userToDelete);
        void ChangeRoleUp(User userToChange);
    }


    public class DbFileService : IFileService
    {
        private readonly AppDBContext? _context;

        public DbFileService(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<FileSharer.ClassLibrary.Entities.File> GetAll()
        {

            return _context.Files.Select(x => new FileSharer.ClassLibrary.Entities.File()
            {
                Id = x.Id,
                FileName = x.FileName,
                FilePath = x.FilePath,
                FileType = x.FileType,
                FileSize = x.FileSize,
                UserId = x.UserId,
                User = new FileSharer.ClassLibrary.Entities.User
                {
                    UserId = x.User.UserId,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                //    Email = x.User.Email,
                //    Avatar = x.User.Avatar,
                //    RoleId = x.User.Role.Id,
                   Role = new FileSharer.ClassLibrary.Entities.Role
                   {
                      //Id = x.User.Role.Id,
                       RoleName = x.User.Role.RoleName
                   }
                    
                }
            });
        }
        public void Add(string fileName, string filePath)
        {
            _context.Files.Add(new Data.EntityF.File() { FileName = fileName, FilePath = filePath});
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var file = _context.Files.Find(id);
            _context.Files.Remove(file);
            _context.SaveChanges();
        }

        public string GetFilePath(int? id)
        {
            File? file = _context.Files.Find(id);

            if (file != null)
            {
                return file.FilePath;
            }
            else
            {
                return "Cписок пуст. Будте первым, кто добавит файл!";
            }


        }

        public string GetFileType(int? id)
        {
            var file = _context.Files.Find(id);
            return file.FileType;
        }

        public bool NullOrNot()
        {
            if (_context.Files.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ClassLibrary.Entities.User IFileService.UsersDBToEntity(int? id)
        {
            var user = _context.Users.Find(id);
            return new ClassLibrary.Entities.User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                // Avatar = user.Avatar,
                RoleId = user.RoleId,
            };
        }
        
        
        public FileModel GetNullFile(FileSharer.ClassLibrary.Entities.File file)
        {
            FileModel NullFileModel = new FileModel
            {
                files = new FileSharer.ClassLibrary.Entities.File[] { file }
            };
            return NullFileModel;
        }


        IEnumerable<ClassLibrary.Entities.User> IFileService.GetAllUsers()
        {
            return _context.Users.Select(x => new FileSharer.ClassLibrary.Entities.User()
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                // Avatar = x.User.Avatar,
                RoleId = x.RoleId,
                Role = new FileSharer.ClassLibrary.Entities.Role
                {
                    //Id = x.User.Role.Id,
                    RoleName = x.Role.RoleName
                }
            });
        }

        User IFileService.GetUser(int? id)
        {
            var user = _context.Users.Find(id);
            return new User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                // Avatar = user.Avatar,
                RoleId = user.RoleId
            };
        }

        public void DeleteUser(User userToDelete)
        {
            var user = _context.Users.Find(userToDelete.UserId);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void ChangeRoleUp(User userToChange)
        {
            var user = _context.Users.Find(userToChange.UserId);
            user.RoleId = user.Role.Id + 1;
            _context.SaveChanges();
        }
    }

}
