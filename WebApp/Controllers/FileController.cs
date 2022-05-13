﻿using Microsoft.AspNetCore.Mvc;
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
        public void DeleteBook(int id)
        {
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
                Data.EntityF.File file = new Data.EntityF.File { fileName = uploadedFile.FileName, filePath = path, fileSize = size, fileType = type };
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}
