using Microsoft.AspNetCore.Mvc;
using FileSharer.ClassLibrary.Entities;
using File = FileSharer.ClassLibrary.Entities.File;
using FileSharer.Web.Services;
using System.Web;
using FileSharer.Web.Data.EntityF;

namespace FileSharer.Web.Controllers
{
    public class FileController : Controller
    {
        public IFileService _fileService;
        private AppDBContext _context;
        private IWebHostEnvironment _environment;

        public FileController(IFileService fileService, AppDBContext context, IWebHostEnvironment environment)
        {
            _fileService = fileService;
            _context = context;
            _environment = environment;
        }


        public IActionResult List()
        {
            var files = _fileService.GetAll();
            ViewBag.Files = files;
            return View(_context.Files.ToList());
        }


        [HttpGet("[controller]/")]
        public IEnumerable<File> GetAll()
        {
            return _fileService.GetAll();
        }

        [HttpDelete("[controller]/")]
        public void DeleteFile(int id)
        {
            // ДОБАВИТЬ,  БЕЗ УДАЛЕНИЯ ФАЙЛА, ТОЛЬКО УДАЛЕНИЕ В БД, НЕ КАСКАДНОЕ
            _fileService.Delete(id);
        }
         
        
        
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                long size = new System.IO.FileInfo(@"C:\Users\Lera\source\repos\Thesis project (Alfer.NET13)\WebApp\wwwroot\" + path).Length;
                string type = new System.IO.FileInfo(path).Extension;
                int userid = 1;  //  ХАРДКОД, ЗАМЕНИТЬ ПРИ АДЕКВАТНОЙ АВТОРИЗАЦИИ ПОЛЬЗОВАТЕЛЯ, ДОБАВИТЬ ПРОВЕРКУ НА РОЛЬ ПОЛЬЗОВАТЕЛЯ 
                Data.EntityF.File file = new Data.EntityF.File { FileName = uploadedFile.FileName, FilePath = path, FileSize = size, FileType = type, UserId = userid, User = _context.Users.Find(userid) };
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}
