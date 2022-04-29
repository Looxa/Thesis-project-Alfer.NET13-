using System;
using System.Collections.Generic;
using FileSharer.ClassLibrary.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = FileSharer.ClassLibrary.Entities.File;

namespace FileSharer.ClassLibrary.Abstract
{
    public interface IFileRepository
    {
        IEnumerable<Entities.File> Files { get; }
        
    }
}
