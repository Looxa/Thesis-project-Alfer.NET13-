namespace FileSharer.Web.Data.EntityF
{
    public class File
    {
        public int fileId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileType { get; set; }
        public long fileSize { get; set; }
        public int userId { get; set; }
        // public string fileDescription { get; set; }   // Нужно-ли?
        // public DateTime fileUploadDate { get; set; }  // Реализовать для организации поиска по дате загрузки
        // public int fileCategoryId { get; set; }      // Если реализую красивое разбиение файлов по папкам
    }
}
