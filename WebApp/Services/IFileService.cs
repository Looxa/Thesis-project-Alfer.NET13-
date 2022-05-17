using FileSharer.Web.Data.EntityF;

namespace FileSharer.Web.Services
{
    public interface IFileService
    {
        public IEnumerable<FileSharer.ClassLibrary.Entities.File> GetAll();

        public void Add(string fileName, string filePath);

        public void Delete(int id);
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
                    Email = x.User.Email,
                    Avatar = x.User.Avatar,
                    RoleId = x.User.RoleId,
                    Role = new FileSharer.ClassLibrary.Entities.Role
                    {
                        Id = x.User.Role.Id,
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
    }

}
