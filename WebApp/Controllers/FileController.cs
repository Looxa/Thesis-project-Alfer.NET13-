using Microsoft.AspNetCore.Mvc;
using FileSharer.ClassLibrary.Entities;
using File = FileSharer.ClassLibrary.Entities.File;
using FileSharer.Web.Services;
using System.Web;
using FileSharer.Web.Data.EntityF;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FileSharer.Web.Models;
using System.Data.Entity;
//using Microsoft.EntityFrameworkCore;


namespace FileSharer.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class FileController : Controller
    {
        public IFileService _fileService;
        private AppDBContext _context;
        private IWebHostEnvironment _environment;
        IEnumerable<Data.EntityF.File> FilesModelFull { get; set; }        

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
                ViewBag.Temp = "Файлов нет, но вы держитесь";
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
            int id = file.Id;
            _fileService.Delete(id);
            return RedirectToAction("List");
        }


        [Authorize(Roles = "Admin, User")]
        public FileStreamResult Download(int? id)
        {
            string file_path = _fileService.GetFilePath(id);
            FileStream file_stream = new FileStream(@"C:\Users\Lera\source\repos\Thesis project (Alfer.NET13)\WebApp\wwwroot\" + file_path, FileMode.Open);
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
                int userid = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
                Data.EntityF.File file = new Data.EntityF.File { FileName = uploadedFile.FileName, FilePath = path, FileSize = size, FileType = type, UserId = userid, User = _context.Users.Find(userid) };
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("List");
        }


        public async Task<IActionResult> SearchFile(string search)
        {
            var files = _fileService.GetAll();
            var foundResult = from f in files select f;
            
            if (!String.IsNullOrEmpty(search))

            {
                foundResult = foundResult.Where(s => s.FileName!.Contains(search));
                if (foundResult.Count() == 0)
                {
                    File file = new File { FileName = "", FilePath = "", FileSize = 0, FileType = "", UserId = 3, User = _fileService.UsersDBToEntity(3) };
                    var NullFileModel =  _fileService.GetNullFile(file);
                    ViewBag.SearchFaild = "Файл не найден ;(";
                    return View(NullFileModel);
                }
            }
            else
            {
                File file = new File { FileName = "", FilePath = "", FileSize = 0, FileType = "", UserId = 3, User = _fileService.UsersDBToEntity(3) };
                FileModel NullFileModel = new FileModel
                {
                    files = new File[] { file }
                };
                ViewBag.SearchFaild = "Файл не найден ;(";
                return View(NullFileModel);
            };

            FileModel ResultFileModel = new FileModel
            {

                files = foundResult.ToList()
            };
            return View(ResultFileModel);

        }
    }
}
