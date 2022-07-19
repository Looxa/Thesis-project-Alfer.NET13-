using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models
{
    public class FileModel
    {

        public int Id { get; set; }


        public IEnumerable<FileSharer.ClassLibrary.Entities.File> files { get; set; }


    }
}
