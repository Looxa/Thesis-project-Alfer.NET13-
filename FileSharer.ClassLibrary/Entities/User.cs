using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharer.ClassLibrary.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string avatar { get; set; }  // url? DB? 
        public string RoleId { get; set; }
    }
}
