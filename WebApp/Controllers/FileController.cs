using Microsoft.AspNetCore.Mvc;
using FileSharer.ClassLibrary.Abstract;
using FileSharer.ClassLibrary.Entities;
using File = FileSharer.ClassLibrary.Entities.File;
using FileSharer.Web.Services;

namespace FileSharer.Web.Controllers
{
    public class FileController : Controller
    {
        public IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }


        public IActionResult List(int fileId, string fileName, string fileType, int fileSize)
        {
            var files = _fileService.GetAll();
            ViewBag.Files = files;
            return View();
        }


        [HttpGet("[controller]/")]
        public IEnumerable<File> GetAll()
        {
            return _fileService.GetAll();
        }

        [HttpDelete("[controller]/")]
        public void DeleteBook(int id)
        {
            _fileService.Delete(id);
        }

        [HttpPost("[controller]/")]
        public void AddBook(string fileName, string fileType, int fileSize)
        {
            _fileService.Add(fileName, fileType, fileSize);
        }

    }
}
