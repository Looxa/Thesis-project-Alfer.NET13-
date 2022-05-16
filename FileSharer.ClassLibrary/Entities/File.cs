using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharer.ClassLibrary.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileType { get; set; }
        public long fileSize { get; set; }
        public int UserId { get; set; }
        // public string fileDescription { get; set; }   // Нужно-ли?
        // public DateTime fileUploadDate { get; set; }  // Реализовать для организации поиска по дате загрузки
        // public int fileCategoryId { get; set; }      // Если реализую красивое разбиение файлов по папкам     

    }
}
