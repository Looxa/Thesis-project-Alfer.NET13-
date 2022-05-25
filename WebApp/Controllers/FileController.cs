using Microsoft.AspNetCore.Mvc;
using FileSharer.ClassLibrary.Entities;
using File = FileSharer.ClassLibrary.Entities.File;
using FileSharer.Web.Services;
using System.Web;
using FileSharer.Web.Data.EntityF;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FileSharer.Web.Models;

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
            FileModel fileModel = new FileModel();
            fileModel.files = _fileService.GetAll();
            
            ViewBag.Files = fileModel.files;
            
            bool filesToTemp = _fileService.NullOrNot();

            if (filesToTemp == true)
            {
                ViewBag.Temp = "Cписок пуст. Будте первым, кто добавит файл!";
            }
            return View(fileModel);


        }


        [HttpGet("[controller]/")]
        public IEnumerable<File> GetAll()
        {
            return _fileService.GetAll();
        }

        [AllowAnonymous]
      //  [Authorize(Roles = "Admin, User")]
        [HttpPost()]
        public async Task<IActionResult> DeleteFile(FileModel fileModel)
        {
            var file = _context.Files.Find(fileModel.Id);
            // ДОБАВИТЬ,  БЕЗ УДАЛЕНИЯ ФАЙЛА, ТОЛЬКО УДАЛЕНИЕ В БД, НЕ КАСКАДНОЕ
            int id = file.Id;
            _fileService.Delete(id);
            return RedirectToAction("List");
        }


        [Authorize(Roles = "Admin, User")]
        public FileStreamResult Download(int? id)
        {
            string file_path = _fileService.GetFilePath(id);
            FileStream file_stream = new FileStream(@"C:\Users\Lera\source\repos\Thesis project (Alfer.NET13)\WebApp\wwwroot\"+file_path, FileMode.Open);
            string file_type = @"aplication/" + _fileService.GetFileType(id).Replace(".", "");
            string file_name = _context.Files.Find(id).FileName;
            return File(file_stream, file_type, file_name);
        }


        [Authorize(Roles = "Admin, User")]
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

                string useridTemp = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
                int userid = int.Parse(useridTemp);
                Data.EntityF.File file = new Data.EntityF.File { FileName = uploadedFile.FileName, FilePath = path, FileSize = size, FileType = type, UserId = userid, User = _context.Users.Find(userid) };
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("List");
        }
        

        public Task<IActionResult> SearchFile(string search)
        {
            //var found = _context.Files.FirstOrDefault<>;
            return null;
        }
    }
}
