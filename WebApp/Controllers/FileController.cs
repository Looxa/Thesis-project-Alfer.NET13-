using Microsoft.AspNetCore.Mvc;
using FileSharer.ClassLibrary.Entities;
using File = FileSharer.ClassLibrary.Entities.File;
using FileSharer.Web.Services;
using System.Web;
using FileSharer.Web.Data.EntityF;
using Microsoft.AspNetCore.Authorization;

namespace FileSharer.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
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

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("[controller]/")]
        public void DeleteFile(int id)
        {
            // ДОБАВИТЬ,  БЕЗ УДАЛЕНИЯ ФАЙЛА, ТОЛЬКО УДАЛЕНИЕ В БД, НЕ КАСКАДНОЕ
            _fileService.Delete(id);
        }


        [Authorize(Roles = "Admin, User")]
        public FileStreamResult Download(int? id)
        {
            //Переделать скачивание файла из вьюхи в контроллер, в соответствии с ролью пользователя
            string file_path = _fileService.GetFilePath(id);
            FileStream file_stream = new FileStream(@"C:\Users\Lera\source\repos\Thesis project (Alfer.NET13)\WebApp\wwwroot\"+file_path, FileMode.Open);
            string file_type = @""+ _fileService.GetFileType(id);
            string file_name = _context.Files.Find(id).FileName;
            return File(file_stream, file_type, file_name);
        }


        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
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
