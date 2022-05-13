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
                fileId = x.fileId,
                fileName = x.fileName,
                filePath = x.filePath,
                fileType = x.fileType,
                fileSize = x.fileSize
            });
        }
        public void Add(string fileName, string filePath)
        {
            _context.Files.Add(new Data.EntityF.File() { fileName = fileName, filePath = filePath});
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
