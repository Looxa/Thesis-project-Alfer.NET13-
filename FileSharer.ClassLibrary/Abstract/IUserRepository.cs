using FileSharer.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharer.ClassLibrary.Abstract
{
    internal interface IUserRepository
    {
        IEnumerable<User> Users { get; }
    }
}
