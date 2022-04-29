using FileSharer.Web.Data.EntityF;

namespace FileSharer.Web.Services
{
    public interface IFileService
    {
        public IEnumerable<FileSharer.ClassLibrary.Entities.File> GetAll();

        public void Add(string fileName, string fileType, int fileSize);

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
                fileId = x.fileId,
                fileName = x.fileName,
                fileType = x.fileType,
                fileSize = x.fileSize
            });
        }
        public void Add(string fileName, string fileType, int fileSize)
        {
            _context.Files.Add(new Data.EntityF.File() { fileName = fileName, fileType = fileType, fileSize = fileSize });
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var file = _context.Files.Find(id);
            _context.Files.Attach(file);
            _context.Files.Remove(file);
            _context.SaveChanges();
        }
    }

}
