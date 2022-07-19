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
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        // public string fileDescription { get; set; }   // Нужно-ли?
        // public DateTime fileUploadDate { get; set; }  // Реализовать для организации поиска по дате загрузки
        // public int fileCategoryId { get; set; }      // Если реализую красивое разбиение файлов по папкам   

    }
}
